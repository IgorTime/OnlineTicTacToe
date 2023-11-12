using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using UnityEngine.SceneManagement;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnAuth)]
    public class OnAuthHandler : IPacketHandler
    {
        public void Handle(INetPacket packet, int connectionId)
        {
            SceneManager.LoadScene("01_Lobby");
        }
    }
}