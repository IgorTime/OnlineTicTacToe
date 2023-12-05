using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnNewRoundHandler : PacketHandler<NetOnNewRound>
    {
        private readonly IGameManager gameManager;
        private readonly IPublisher<OnGameRestart> onGameRestartPublisher;

        public OnNewRoundHandler(
            IGameManager gameManager,
            IPublisher<OnGameRestart> onGameRestartPublisher)
        {
            this.gameManager = gameManager;
            this.onGameRestartPublisher = onGameRestartPublisher;
        }
        
        protected override void Handle(NetOnNewRound message, int connectionId)
        {
            gameManager.Reset();
            gameManager.InputEnabled = true;
            
            onGameRestartPublisher.Publish(new OnGameRestart());
        }
    }
}