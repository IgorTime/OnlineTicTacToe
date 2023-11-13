using System;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnServerStatus)]
    public class OnServerStatusRequestHandler : IPacketHandler
    {
        public static Action<NetOnServerStatus> OnServerStatus;

        [InitializeOnLoadMethod]
        private static void ResetState()
        {
            OnServerStatus = null;
        }
        
        public void Handle(INetPacket packet, int connectionId)
        {
            if (SceneManager.GetActiveScene().name != "01_Lobby")
            {
                return;
            }
            
            var msg = (NetOnServerStatus) packet;
            OnServerStatus?.Invoke(msg);
        }
    }
}