using MessagePipe;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnAuthFailHandler : PacketHandler<NetOnAuthFail>
    {
        private readonly IPublisher<OnAuthFailed> publisher;

        public OnAuthFailHandler(IPublisher<OnAuthFailed> publisher)
        {
            this.publisher = publisher;
        }

        protected override void Handle(NetOnAuthFail packet, int connectionId)
        {
            publisher.Publish(new OnAuthFailed());
        }
    }
}