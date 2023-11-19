using LiteNetLib.Utils;

namespace TTC.Shared.Extensions
{
    public static class WriterExtensions
    {
        public static NetDataWriter SerializeNetPacket<T>(this NetDataWriter writer, T packet)
            where T : struct, INetPacket
        {
            writer.Reset();
            writer.Put((byte) packet.Type);
            packet.Serialize(writer);
            return writer;
        }
    }
}