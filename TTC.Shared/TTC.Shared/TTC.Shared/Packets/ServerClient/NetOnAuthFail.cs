using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ServerClient
{
    public struct NetOnAuthFail : INetPacket
    {
        public PacketType Type => PacketType.OnAuthFail;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte) Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}