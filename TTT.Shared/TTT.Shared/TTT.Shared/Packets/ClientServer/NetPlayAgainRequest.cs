using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetPlayAgainRequest : INetPacket
    {
        public PacketType Type => PacketType.PlayAgainRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}