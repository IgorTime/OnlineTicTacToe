using System;
using MessagePipe;
using TMPro;
using TTT.Client.LocalMessages;
using TTT.Client.Services;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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
        private RectTransform loadingPanel;

        [Header("Buttons")]
        [SerializeField]
        private Button logoutButton;

        [SerializeField]
        private Button findOpponentButton;

        [SerializeField]
        private Button cancelFindOpponentButton;

        private INetworkClient networkClient;
        private ISceneLoader sceneLoader;
        private IDisposable unsubscribe;

        [Inject]
        public void Construct(
            INetworkClient networkClient,
            ISceneLoader sceneLoader,
            ISubscriber<OnServerStatusUpdated> onServerStatusUpdatedSubscriber)
        {
            this.networkClient = networkClient;
            this.sceneLoader = sceneLoader;
            
            var bag = DisposableBag.CreateBuilder();
            onServerStatusUpdatedSubscriber.Subscribe(Refresh).AddTo(bag);
            unsubscribe = bag.Build();
            
            RequestServerStatus();
        }

        private void Start()
        {
            logoutButton.onClick.AddListener(OnLogoutButtonClicked);
            findOpponentButton.onClick.AddListener(OnFindOpponentButtonClicked);
            cancelFindOpponentButton.onClick.AddListener(OnCancelFindOpponentButtonClicked);
        }

        private void OnDestroy()
        {
            unsubscribe?.Dispose();
            logoutButton.onClick.RemoveListener(OnLogoutButtonClicked);
        }

        private void OnCancelFindOpponentButtonClicked()
        {
            findOpponentButton.gameObject.SetActive(true);
            loadingPanel.gameObject.SetActive(false);
            var msg = new NetCancelFindOpponentRequest();
            networkClient.SendServer(msg);
        }

        private void OnFindOpponentButtonClicked()
        {
            findOpponentButton.gameObject.SetActive(false);
            loadingPanel.gameObject.SetActive(true);
            var msg = new NetFindOpponentRequest();
            networkClient.SendServer(msg);
        }

        private void OnLogoutButtonClicked()
        {
            networkClient.Disconnect();
            sceneLoader.LoadLoginScene();
        }

        private void RequestServerStatus()
        {
            var msg = new NetServerStatusRequest();
            networkClient.SendServer(msg);
        }

        private void Refresh(OnServerStatusUpdated message)
        {
            // TODO this is naive and fast version. Rewrite for production
            DestroyAllChildren(topPlayersContainer);
            CreateTopPlayers(message.TopPlayers);
            
            onlinePlayersCount.text = $"{message.PlayersCount} players online";
        }

        private void CreateTopPlayers(PlayerNetDto[] topPlayers)
        {
            for (var i = 0; i < topPlayers.Length; i++)
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
    }
}