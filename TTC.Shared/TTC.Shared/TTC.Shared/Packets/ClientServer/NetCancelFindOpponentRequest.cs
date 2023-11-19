using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ClientServer
{
    public struct NetCancelFindOpponentRequest : INetPacket
    {
        public PacketType Type => PacketType.CancelFindOpponentRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}