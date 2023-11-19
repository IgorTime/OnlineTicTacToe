using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ClientServer
{
    public struct NetFindOpponentRequest : INetPacket
    {
        public PacketType Type => PacketType.FindOpponentRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}