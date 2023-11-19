using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;
using UnityEngine.SceneManagement;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnAuth)]
    public class OnAuthHandler : PacketHandler<NetOnAuth>
    {
        protected override void Handle(NetOnAuth packet, int connectionId)
        {
            SceneManager.LoadScene("01_Lobby");
        }
    }
}