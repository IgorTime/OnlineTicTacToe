using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;
using UnityEngine.SceneManagement;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnStartGame)]
    public class OnStartGameHandler : PacketHandler<NetOnStartGame>
    {
        protected override void Handle(NetOnStartGame packet, int connectionId)
        {
            SceneManager.LoadScene("02_Game");
        }
    }
}