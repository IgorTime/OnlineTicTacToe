using System;
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

        private readonly Dictionary<byte, Type> handlersMap = new()
        {
            [(byte) PacketType.OnServerStatus] = typeof(OnServerStatusRequestHandler),
            [(byte) PacketType.OnAuth] = typeof(OnAuthHandler),
            [(byte) PacketType.OnStartGame] = typeof(OnStartGameHandler),
            [(byte) PacketType.OnMarkCell] = typeof(OnMarkCellHandler),
        };

        public PacketHandlerResolver(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        public IPacketHandler Resolve(PacketType packetType)
        {
            var packetByte = (byte) packetType;
            if (resolvedHandlers.TryGetValue(packetByte, out var handler))
            {
                return handler;
            }

            var handlerType = handlersMap[packetByte];
            return resolvedHandlers[packetByte] = (IPacketHandler)objectResolver.ActivateInstance(handlerType);
        }
    }
}