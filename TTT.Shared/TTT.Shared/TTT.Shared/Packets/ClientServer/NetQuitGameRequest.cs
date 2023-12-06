using LiteNetLib.Utils;

namespace TTT.Shared.Packets.ClientServer
{
    public struct NetQuitGameRequest : INetPacket
    {
        public PacketType Type => PacketType.QuitGameRequest;

        public void Serialize(NetDataWriter writer)
        {
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}