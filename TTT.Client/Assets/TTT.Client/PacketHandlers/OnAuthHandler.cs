using TTT.Client.Services;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnAuthHandler : PacketHandler<NetOnAuth>
    {
        private readonly ISceneLoader sceneLoader;

        public OnAuthHandler(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        protected override void Handle(NetOnAuth packet, int connectionId)
        {
            sceneLoader.LoadLobbyScene();
        }
    }
}