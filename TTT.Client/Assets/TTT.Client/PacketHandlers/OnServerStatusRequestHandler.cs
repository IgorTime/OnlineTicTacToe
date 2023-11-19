﻿using System;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TTT.Client.PacketHandlers
{
    [HandlerRegister(PacketType.OnServerStatus)]
    public class OnServerStatusRequestHandler : PacketHandler<NetOnServerStatus>
    {
        public static Action<NetOnServerStatus> OnServerStatus;

        [RuntimeInitializeOnLoadMethod]
        private static void ResetState()
        {
            OnServerStatus = null;
        }

        protected override void Handle(NetOnServerStatus packet, int connectionId)
        {
            if (SceneManager.GetActiveScene().name != "01_Lobby")
            {
                return;
            }

            OnServerStatus?.Invoke(packet);
        }
    }
}