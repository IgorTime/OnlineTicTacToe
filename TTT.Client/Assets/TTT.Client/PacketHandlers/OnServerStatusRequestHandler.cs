using MessagePipe;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnServerStatusRequestHandler : PacketHandler<NetOnServerStatus>
    {
        private readonly IPublisher<OnServerStatusUpdated> onServerStatusUpdatedPublisher;

        public OnServerStatusRequestHandler(IPublisher<OnServerStatusUpdated> onServerStatusUpdatedPublisher)
        {
            this.onServerStatusUpdatedPublisher = onServerStatusUpdatedPublisher;
        }

        protected override void Handle(NetOnServerStatus packet, int connectionId)
        {
            onServerStatusUpdatedPublisher.Publish(new OnServerStatusUpdated()
            {
                PlayersCount = packet.PlayersCount,
                TopPlayers = packet.TopPlayers,
            });
        }
    }
}