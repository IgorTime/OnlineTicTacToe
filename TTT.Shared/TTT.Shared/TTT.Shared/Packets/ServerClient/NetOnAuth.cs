using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ServerClient
{
    public struct NetOnAuth : INetPacket
    {
        public PacketType Type => PacketType.OnAuth;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}