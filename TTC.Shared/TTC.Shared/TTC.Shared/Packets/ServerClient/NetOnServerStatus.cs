using LiteNetLib.Utils;

namespace TTC.Shared.Packets.ServerClient
{
    public struct NetOnServerStatus : INetPacket
    {
        public PacketType Type => PacketType.OnServerStatus;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte) Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}