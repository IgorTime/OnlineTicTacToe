using System;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;

namespace PacketHandlers
{
    [HandlerRegister(PacketType.OnAuthFail)]
    public class OnAuthFailHandler : IPacketHandler
    {
        public static event Action<NetOnAuthFail> OnAuthFail;
        public void Handle(INetPacket packet, int connectionId)
        {
            var message = (NetOnAuthFail) packet;
            OnAuthFail?.Invoke(message);
        }
    }
}