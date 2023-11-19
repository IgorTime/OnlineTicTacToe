using LiteNetLib.Utils;

namespace TTT.Shared
{
    public interface INetPacket : INetSerializable
    {
        PacketType Type { get; }
    }
}