using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetServerStatusRequest : INetPacket
    {
        public PacketType Type => PacketType.ServerStatusRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}