using LiteNetLib.Utils;
using SharedLib;

namespace TicTacServer.PacketHandlers
{
    public struct Net_OnAuth : INetPacket
    {
        public PacketType Type => PacketType.OnAuth;

        public void Serialize(NetDataWriter writer)
        {
           writer.Put((byte)Type);
        }

        public void Deserialize(NetDataReader reader)
        {
           
        }
    }
}