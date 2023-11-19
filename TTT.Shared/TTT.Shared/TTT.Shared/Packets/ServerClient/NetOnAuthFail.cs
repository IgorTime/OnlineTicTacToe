using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnAuthFail : INetPacket
    {
        public PacketType Type => PacketType.OnAuthFail;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}