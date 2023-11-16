using System;
using TMPro;
using TTC.Shared.Packets.ClientServer;
using TTC.Shared.Packets.ServerClient;
using TTT.Client.PacketHandlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TTT.Client.Lobby
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField]
        private Transform topPlayersContainer;
        
        [SerializeField]
        private PlayerRow topPlayerPrefab;

        [SerializeField] 
        private TextMeshProUGUI onlinePlayersCount;

        [SerializeField] 
        private Button logoutButton;
        
        private void Start()
        {
            OnServerStatusRequestHandler.OnServerStatus += Refresh;
            RequestServerStatus();
            logoutButton.onClick.AddListener(OnLogoutButtonClicked);
        }

        private void OnLogoutButtonClicked()
        {
            NetworkClient.Instance.Disconnect();
            SceneManager.LoadScene("00_Main");
        }

        private void OnDestroy()
        {
            OnServerStatusRequestHandler.OnServerStatus -= Refresh;
            logoutButton.onClick.RemoveListener(OnLogoutButtonClicked);
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
            onlinePlayersCount.text = $"{message.PlayersCount} players online";
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
