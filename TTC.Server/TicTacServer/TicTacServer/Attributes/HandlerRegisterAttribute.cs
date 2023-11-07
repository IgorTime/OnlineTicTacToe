using TTC.Shared;

namespace TicTacServer.PacketHandlers;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerRegisterAttribute : Attribute
{
    public PacketType PacketType { get; }

    public HandlerRegisterAttribute(PacketType packetType)
    {
        PacketType = packetType;
    }
}