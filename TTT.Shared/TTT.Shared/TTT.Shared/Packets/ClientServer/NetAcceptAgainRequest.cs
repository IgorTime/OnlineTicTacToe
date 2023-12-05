using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetAcceptAgainRequest : INetPacket
    {
        public PacketType Type => PacketType.AcceptPlayAgainRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}