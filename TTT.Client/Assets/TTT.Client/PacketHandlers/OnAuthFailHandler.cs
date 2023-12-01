using System;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnAuthFailHandler : PacketHandler<NetOnAuthFail>
    {
        public static event Action<NetOnAuthFail> OnAuthFail;

        protected override void Handle(NetOnAuthFail packet, int connectionId)
        {
            OnAuthFail?.Invoke(packet);
        }
    }
}