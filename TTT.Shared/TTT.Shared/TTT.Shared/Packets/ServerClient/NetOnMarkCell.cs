using LiteNetLib.Utils;
using TTT.Shared.Models;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnMarkCell : INetPacket
    {
        public PacketType Type => PacketType.OnMarkCell;

        public string Actor { get; set; }
        public byte Index { get; set; }
        public MarkOutcome Outcome { get; set; }
        public WinLine WinLine { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Actor);
            writer.Put(Index);
            writer.Put((byte) Outcome);
            writer.Put((byte) WinLine);
        }

        public void Deserialize(NetDataReader reader)
        {
            Actor = reader.GetString();
            Index = reader.GetByte();
            Outcome = (MarkOutcome) reader.GetByte();
            WinLine = (WinLine) reader.GetByte();
        }
    }
}