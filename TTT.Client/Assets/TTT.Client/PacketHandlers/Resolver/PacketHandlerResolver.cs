using System.Collections.Generic;
using TTT.Client.Extensions;
using TTT.Shared;
using TTT.Shared.Handlers;
using VContainer;

namespace TTT.Client.PacketHandlers
{
    public class PacketHandlerResolver : IPacketHandlerResolver
    {
        private readonly IObjectResolver objectResolver;
        private readonly Dictionary<byte, IPacketHandler> resolvedHandlers = new();

        public PacketHandlerResolver(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        public IPacketHandler Resolve(PacketType packetType)
        {
            if (resolvedHandlers.TryGetValue((byte) packetType, out var handler))
            {
                return handler;
            }

            return resolvedHandlers[(byte) packetType] = CreateHandler(packetType);
        }

        private IPacketHandler CreateHandler(PacketType packetType)
        {
            return packetType switch
            {
                PacketType.OnServerStatus => objectResolver.ActivateInstance<OnServerStatusRequestHandler>(),
                PacketType.OnAuth => objectResolver.ActivateInstance<OnAuthHandler>(),
                PacketType.OnStartGame => objectResolver.ActivateInstance<OnStartGameHandler>(),
                _ => null,
            };
        }
    }
}