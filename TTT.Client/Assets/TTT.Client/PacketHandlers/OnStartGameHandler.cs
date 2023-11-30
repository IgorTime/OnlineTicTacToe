using TTT.Client.Gameplay;
using TTT.Client.Services;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnStartGame)]
    public class OnStartGameHandler : PacketHandler<NetOnStartGame>
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IGameManager gameManager;

        public OnStartGameHandler(
            ISceneLoader sceneLoader,
            IGameManager gameManager)
        {
            this.sceneLoader = sceneLoader;
            this.gameManager = gameManager;
        }

        protected override void Handle(NetOnStartGame packet, int connectionId)
        {
            gameManager.RegisterGame(packet.GameId, packet.XUser, packet.OUser);
            sceneLoader.LoadGameScene();
        }
    }
}