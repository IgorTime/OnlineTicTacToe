using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnNewRound : INetPacket
    {
        public PacketType Type => PacketType.OnNewRound;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}