using System;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnServerStatusRequestHandler : PacketHandler<NetOnServerStatus>
    {
        public static Action<NetOnServerStatus> OnServerStatus;

        protected override void Handle(NetOnServerStatus packet, int connectionId)
        {
            OnServerStatus?.Invoke(packet);
        }
    }
}