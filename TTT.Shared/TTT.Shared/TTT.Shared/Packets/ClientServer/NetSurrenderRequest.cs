using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetSurrenderRequest : INetPacket
    {
        public PacketType Type => PacketType.SurrenderRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}