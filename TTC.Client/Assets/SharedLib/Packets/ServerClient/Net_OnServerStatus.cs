using LiteNetLib.Utils;
using SharedLib;

namespace TicTacServer.PacketHandlers
{
    internal struct Net_OnServerStatus : INetPacket
    {
        public PacketType Type => PacketType.OnServerStatus;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}