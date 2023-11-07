using LiteNetLib.Utils;
using SharedLib;

namespace TicTacServer.PacketHandlers
{
    public struct Net_OnAuthFail : INetPacket
    {
        public PacketType Type => PacketType.OnAuthFail;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Type);
        }

        public void Deserialize(NetDataReader reader)
        {
        }
    }
}