using System;

namespace TTC.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerRegisterAttribute : Attribute
    {
        public PacketType PacketType { get; }

        public HandlerRegisterAttribute(PacketType packetType)
        {
            PacketType = packetType;
        }
    }
}