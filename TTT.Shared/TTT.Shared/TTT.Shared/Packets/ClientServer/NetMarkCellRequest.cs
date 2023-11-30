using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetMarkCellRequest : INetPacket
    {
        public PacketType Type => PacketType.MarkCellRequest;

        public byte Index { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
        }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetByte();
        }
    }
}