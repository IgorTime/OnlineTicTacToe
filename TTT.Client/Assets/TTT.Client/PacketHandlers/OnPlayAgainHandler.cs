using MessagePipe;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnPlayAgainHandler : PacketHandler<NetOnPlayAgain>
    {
        private readonly IPublisher<OnPlayAgain> onPlayAgainPublisher;

        public OnPlayAgainHandler(IPublisher<OnPlayAgain> onPlayAgainPublisher)
        {
            this.onPlayAgainPublisher = onPlayAgainPublisher;
        }

        protected override void Handle(NetOnPlayAgain message, int connectionId)
        {
            onPlayAgainPublisher.Publish(new OnPlayAgain());
        }
    }
}