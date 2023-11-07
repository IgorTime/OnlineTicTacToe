using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using TTC.Shared;
using TTC.Shared.Handlers;
using TTC.Shared.Registries;
using UnityEngine;

public class NetworkClient : MonoBehaviour, INetEventListener
{
    private NetManager netManager;
    private NetPeer server;
    private NetDataWriter writer;
    private PacketRegistry packetRegistry;
    private PacketHandlerRegistry handlerRegistry;

    public static NetworkClient Instance { get; private set; }
    public bool IsConnected => server != null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        netManager?.PollEvents();
    }

    public void Connect()
    {
        netManager.Connect("localhost", 9050, "");
    }

    public void SendServer<T>(T packed, DeliveryMethod method = DeliveryMethod.ReliableOrdered)
        where T : INetPacket
    {
        if (server != null)
        {
            writer.Reset();
            packed.Serialize(writer);
            server.Send(writer, method);
        }
        else
        {
            Debug.Log("Not connected to server");
        }
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log($"We connected to the server at {peer.EndPoint}");
        server = peer;
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("Lost connection to the server");
        server = null;
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
        var packetType = (PacketType) reader.GetByte();
        var packet = ResolvePacket(packetType, reader);
        var handler = ResolveHandler(packetType);
        handler.Handle(packet, peer.Id);
        reader.Recycle();
    }

    private IPacketHandler ResolveHandler(PacketType packetType)
    {
        // TODO refactor this
        var type = handlerRegistry[packetType];
        var handler = (IPacketHandler) Activator.CreateInstance(type);
        return handler;
    }

    private INetPacket ResolvePacket(PacketType packetType, NetPacketReader reader)
    {
        //TODO refactor this
        var type = packetRegistry[packetType];
        var packet = (INetPacket) Activator.CreateInstance(type);
        return packet;
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
    }

    private void Init()
    {
        packetRegistry = new PacketRegistry();
        handlerRegistry = new PacketHandlerRegistry();
        writer = new NetDataWriter();
        netManager = new NetManager(this)
        {
            DisconnectTimeout = 10_000,
        };

        netManager.Start();
    }
}