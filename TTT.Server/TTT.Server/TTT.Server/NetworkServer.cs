using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TTT.Server.Game;
using TTT.Shared;
using TTT.Shared.Extensions;
using TTT.Shared.Handlers;
using TTT.Shared.Registries;

namespace TTT.Server;

public class NetworkServer : INetEventListener
{
    private readonly ILogger<NetworkServer> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly UsersManager usersManager;
    private readonly NetManager netManager;
    private readonly NetDataWriter writer = new();

    public NetworkServer(
        ILogger<NetworkServer> logger,
        IServiceProvider serviceProvider,
        UsersManager usersManager)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.usersManager = usersManager;
        netManager = new NetManager(this)
        {
            DisconnectTimeout = 10_000,
        };
    }

    public void Start()
    {
        netManager.Start(9050);
        Console.WriteLine("Server started in port 9050");
    }

    public void PollEvents()
    {
        netManager.PollEvents();
    }

    public void OnPeerConnected(NetPeer peer)
    {
        logger.LogInformation($"Client connected to server: {peer.EndPoint}. " +
                              $"Id: {peer.Id}");

        usersManager.AddConnection(peer);
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        var connection = usersManager.GetConnection(peer.Id);
        netManager.DisconnectPeer(peer);
        usersManager.Disconnect(peer.Id, this);
        logger.LogInformation($"{connection?.User?.Id} disconnected: {peer.EndPoint}");
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
    }

    public void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    {
        using var scope = serviceProvider.CreateScope();
        var packetType = reader.GetByte();

        try
        {
            var handler = ResolveHandler((PacketType) packetType);
            handler.Handle(reader, peer.Id);
            reader.Recycle();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error while handling packet of type {(PacketType)packetType}");
        }
    }

    public void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine("Incoming connection from " + request.RemoteEndPoint);
        request.Accept();
    }

    public void SendClient<T>(
        int peerId,
        T packet,
        DeliveryMethod method = DeliveryMethod.ReliableOrdered)
        where T : struct, INetPacket
    {
        var peer = usersManager.GetConnection(peerId).Peer;
        peer.Send(writer.SerializeNetPacket(packet), method);
    }

    private IPacketHandler ResolveHandler(PacketType packetType)
    {
        var registry = serviceProvider.GetRequiredService<PacketHandlerRegistry>();
        var type = registry[packetType];
        return (IPacketHandler) serviceProvider.GetRequiredService(type);
    }
}