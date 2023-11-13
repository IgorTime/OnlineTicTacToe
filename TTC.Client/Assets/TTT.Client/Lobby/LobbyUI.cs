using TTC.Shared.Packets.ClientServer;
using TTC.Shared.Packets.ServerClient;
using TTT.Client.PacketHandlers;
using UnityEngine;

namespace TTT.Client.Lobby
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField]
        private Transform topPlayersContainer;
        
        [SerializeField]
        private PlayerRow topPlayerPrefab;
        
        private void Start()
        {
            OnServerStatusRequestHandler.OnServerStatus += Refresh;
            RequestServerStatus();
        }

        private void RequestServerStatus()
        {
            var msg = new NetServerStatusRequest();
            NetworkClient.Instance.SendServer(msg);
        }

        private void Refresh(NetOnServerStatus message)
        {
            DestroyAllChildren(topPlayersContainer);
            CreateTopPlayers(message.TopPlayers);
        }

        private void CreateTopPlayers(PlayerNetDto[] topPlayers)
        {
            for (int i = 0; i < topPlayers.Length; i++)
            {
                var player = topPlayers[i];
                var playerUI = Instantiate(topPlayerPrefab, topPlayersContainer);
                playerUI.Initialize(player);
            }
        }

        private void DestroyAllChildren(Transform rectTransform)
        {
            while (rectTransform.childCount > 0)
            {
                DestroyImmediate(rectTransform.GetChild(0).gameObject);
            }
        }

        // Find Opponents
        // Cancel find opponent method
        // Logout
        // Refresh ui
    }
}
