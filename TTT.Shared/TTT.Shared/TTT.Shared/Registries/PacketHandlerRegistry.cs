using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;

namespace TTT.Shared.Registries
{
    public class PacketHandlerRegistry
    {
        private readonly Dictionary<byte, Type> handlersMap = new();

        public Type this[PacketType packetType] => handlersMap[(byte) packetType];

        public PacketHandlerRegistry()
        {
            Initialize();
        }

        private void Initialize()
        {
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(x => x.DefinedTypes)
                                    .Where(t => t is {IsInterface: false, IsAbstract: false})
                                    .Where(x => typeof(IPacketHandler).IsAssignableFrom(x))
                                    .Select(t => (type: t, attibute: t.GetCustomAttribute<HandlerRegisterAttribute>()))
                                    .Where(x => x.attibute != null);

            foreach (var (type, attribute) in handlers)
            {
                if (!handlersMap.ContainsKey((byte) attribute.PacketType))
                {
                    handlersMap[(byte) attribute.PacketType] = type;
                }
                else
                {
                    throw new Exception($"Multiple handlers for the same packet type {attribute.PacketType}");
                }
            }
        }
    }
}