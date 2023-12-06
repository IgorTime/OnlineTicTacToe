using MessagePipe;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnSurrenderHandler : PacketHandler<NetOnSurrender>
    {
        private readonly IPublisher<OnSurrender> onSurrenderPublisher;

        public OnSurrenderHandler(IPublisher<OnSurrender> onSurrenderPublisher)
        {
            this.onSurrenderPublisher = onSurrenderPublisher;
        }

        protected override void Handle(NetOnSurrender message, int connectionId)
        {
            onSurrenderPublisher.Publish(new OnSurrender
            {
                Winner = message.WinnerName,
            });
        }
    }
}