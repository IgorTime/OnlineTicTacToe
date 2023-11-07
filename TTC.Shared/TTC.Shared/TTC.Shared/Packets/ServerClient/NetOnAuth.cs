using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ServerClient
{
    public struct NetOnAuth : INetPacket
    {
        public PacketType Type => PacketType.OnAuth;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte) Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}