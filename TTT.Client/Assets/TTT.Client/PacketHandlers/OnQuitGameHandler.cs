using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Client.Services;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnQuitGameHandler : PacketHandler<NetOnQuitGame>
    {
        private readonly IGameManager gameManager;
        private readonly ISceneLoader sceneLoader;
        private readonly IPublisher<OnOpponentQuitGame> onQuitGamePublisher;

        public OnQuitGameHandler(
            IGameManager gameManager,
            ISceneLoader sceneLoader,
            IPublisher<OnOpponentQuitGame> onQuitGamePublisher)
        {
            this.gameManager = gameManager;
            this.sceneLoader = sceneLoader;
            this.onQuitGamePublisher = onQuitGamePublisher;
        }
        
        protected override void Handle(NetOnQuitGame message, int connectionId)
        {
            if (gameManager.MyUsername == message.QuitterName)
            {
                sceneLoader.LoadLobbyScene();
                return;
            }
            
            onQuitGamePublisher.Publish(new OnOpponentQuitGame
            {
                Quitter = message.QuitterName,
            });
        }
    }
}