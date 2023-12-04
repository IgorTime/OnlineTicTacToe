using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnPlayAgain : INetPacket
    {
        public PacketType Type => PacketType.OnPlayAgain;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}