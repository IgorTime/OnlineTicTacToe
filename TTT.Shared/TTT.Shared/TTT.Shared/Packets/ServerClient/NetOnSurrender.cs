using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnSurrender : INetPacket
    {
        public PacketType Type => PacketType.OnSurrender;

        public string WinnerName { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(WinnerName);
        }

        public void Deserialize(NetDataReader reader)
        {
            WinnerName = reader.GetString();
        }
    }
}