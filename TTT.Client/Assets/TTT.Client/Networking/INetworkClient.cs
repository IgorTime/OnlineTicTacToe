using LiteNetLib;
using TTT.Shared;

namespace TTT.Client
{
    public interface INetworkClient
    {
        bool IsConnected { get; }

        void Connect();

        void Disconnect();

        void SendServer<T>(T packed, DeliveryMethod method = DeliveryMethod.ReliableOrdered)
            where T : struct, INetPacket;
    }
}