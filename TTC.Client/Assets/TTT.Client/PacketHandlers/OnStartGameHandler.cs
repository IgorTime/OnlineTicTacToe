using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;
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