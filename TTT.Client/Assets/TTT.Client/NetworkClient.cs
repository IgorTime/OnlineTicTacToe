using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using TTT.Client.PacketHandlers;
using TTT.Shared;
using TTT.Shared.Extensions;
using TTT.Shared.Registries;
using UnityEngine;
using VContainer.Unity;

namespace TTT.Client
{
    public class NetworkClient :
        INetworkClient,
        INetEventListener,
        IStartable,
        ITickable,
        IDisposable
    {
        private readonly IPacketHandlerResolver packetHandlerResolver;
        private PacketHandlerRegistry handlerRegistry;
        private NetManager netManager;
        private NetPeer server;
        private NetDataWriter writer;

        public bool IsConnected => server != null;

        public NetworkClient(
            IPacketHandlerResolver packetHandlerResolver)
        {
            this.packetHandlerResolver = packetHandlerResolver;
        }
        
        public void Start()
        {
            Init();
        }

        public void Tick()
        {
            netManager?.PollEvents();
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
            var handler = packetHandlerResolver.Resolve(packetType);
            handler.Handle(reader, peer.Id);
            reader.Recycle();
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

        public void Connect()
        {
            netManager.Connect("localhost", 9050, "");
        }

        public void SendServer<T>(T packed, DeliveryMethod method = DeliveryMethod.ReliableOrdered)
            where T : struct, INetPacket
        {
            if (server != null)
            {
                server.Send(writer.SerializeNetPacket(packed), method);
            }
            else
            {
                Debug.Log("Not connected to server");
            }
        }

        public void Disconnect()
        {
            netManager?.DisconnectAll();
        }

        public void Dispose()
        {
            netManager?.Stop();
        }

        private void Init()
        {
            handlerRegistry = new PacketHandlerRegistry();
            writer = new NetDataWriter();
            netManager = new NetManager(this)
            {
                DisconnectTimeout = 10_000,
            };

            netManager.Start();
        }
    }
}