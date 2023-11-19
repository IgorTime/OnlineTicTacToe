using System;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnAuthFail)]
    public class OnAuthFailHandler : PacketHandler<NetOnAuthFail>
    {
        public static event Action<NetOnAuthFail> OnAuthFail;

        protected override void Handle(NetOnAuthFail packet, int connectionId)
        {
            OnAuthFail?.Invoke(packet);
        }
    }
}