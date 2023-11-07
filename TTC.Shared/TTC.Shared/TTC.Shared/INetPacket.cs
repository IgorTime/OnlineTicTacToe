using LiteNetLib.Utils;

namespace TTC.Shared
{
    public interface INetPacket : INetSerializable
    {
        PacketType Type { get; }
    }
}