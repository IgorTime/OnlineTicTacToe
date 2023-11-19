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

        public OnStartGameHandler(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        protected override void Handle(NetOnStartGame packet, int connectionId)
        {
            sceneLoader.LoadGameScene();
        }
    }
}