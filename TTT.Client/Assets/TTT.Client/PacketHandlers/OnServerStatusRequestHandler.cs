using System;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnServerStatus)]
    public class OnServerStatusRequestHandler : PacketHandler<NetOnServerStatus>
    {
        public static Action<NetOnServerStatus> OnServerStatus;

        protected override void Handle(NetOnServerStatus packet, int connectionId)
        {
            OnServerStatus?.Invoke(packet);
        }
    }
}