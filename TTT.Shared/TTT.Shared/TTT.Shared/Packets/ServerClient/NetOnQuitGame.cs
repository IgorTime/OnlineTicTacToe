using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnQuitGame : INetPacket
    {
        public PacketType Type => PacketType.OnQuitGame;
        
        public string QuitterName { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(QuitterName);
        }

        public void Deserialize(NetDataReader reader)
        {
            QuitterName = reader.GetString();
        }
    }
}