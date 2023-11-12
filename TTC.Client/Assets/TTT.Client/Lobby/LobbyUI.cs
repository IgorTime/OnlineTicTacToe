using System;
using TTC.Shared.Packets.ClientServer;
using UnityEngine;

namespace TTT.Client.Lobby
{
    public class LobbyUI : MonoBehaviour
    {
        private void Start()
        {
            RequestServerStatus();
        }

        private void RequestServerStatus()
        {
            var msg = new NetServerStatusRequest();
            NetworkClient.Instance.SendServer(msg);
        }

        // Find Opponents
        // Cancel find opponent method
        // Logout
        // Refresh ui
    }
}
