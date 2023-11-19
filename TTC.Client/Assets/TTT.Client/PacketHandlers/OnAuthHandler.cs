using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;
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