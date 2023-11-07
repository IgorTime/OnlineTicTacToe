using System;
using System.Collections.Generic;
using System.Linq;

namespace TTC.Shared.Registries
{
    public class PacketRegistry
    {
        private readonly Dictionary<byte, Type> packetTypes = new(32);

        public Type this[PacketType value] => packetTypes[(byte) value];

        public PacketRegistry()
        {
            Initialize();
        }

        private void Initialize()
        {
            var packetType = typeof(INetPacket);
            var packets = AppDomain.CurrentDomain
                                   .GetAssemblies()
                                   .SelectMany(x => x.GetTypes())
                                   .Where(t => packetType.IsAssignableFrom(t) &&
                                               t is {IsInterface: false, IsAbstract: false});

            foreach (var packet in packets)
            {
                var instance = (INetPacket) Activator.CreateInstance(packet);
                packetTypes.Add((byte) instance.Type, packet);
            }
        }
    }
}