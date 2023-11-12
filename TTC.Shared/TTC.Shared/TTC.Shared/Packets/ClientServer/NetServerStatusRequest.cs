using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ClientServer
{
    public struct NetServerStatusRequest : INetPacket
    {
        public PacketType Type => PacketType.ServerStatusRequest;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}