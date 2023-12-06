using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Shared.Models;
using TTT.Shared.Packets.ClientServer;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TTT.Client.Game
{
    public class GameUI : MonoBehaviour
    {
        [Header("Header:")]
        [SerializeField]
        private UserView xUserView;

        [SerializeField]
        private UserView oUserView;

        [SerializeField]
        private TurnUI turnUI;
        
        [Header("End game popup:")]
        [SerializeField]
        private EndGamePopup endGamePopup;
        
        [SerializeField]
        private float showPopupDelay = 1.5f;

        [Header("Footer:")]
        [SerializeField]
        private Button surrenderButton;

        private IGameManager gameManager;
        private IDisposable unSubscriber;
        private INetworkClient networkClient;

        [Inject]
        public void Construct(
            IGameManager gameManager,
            INetworkClient networkClient,
            ISubscriber<OnCellMarked> onCellMarked,
            ISubscriber<OnGameRestart> onGameRestart)
        {
            this.gameManager = gameManager;
            this.networkClient = networkClient;
            
            var bag = DisposableBag.CreateBuilder();
            onCellMarked.Subscribe(OnCellMarked).AddTo(bag);
            onGameRestart.Subscribe(OnGameRestart).AddTo(bag);
            unSubscriber = bag.Build();
        }

        private void Start()
        {
            InitHeader();
            turnUI.SetTurn(gameManager.IsMyTurn);
            surrenderButton.onClick.AddListener(OnSurrenderClick);
        }

        private void OnDestroy()
        {
            unSubscriber?.Dispose();
            surrenderButton.onClick.RemoveListener(OnSurrenderClick);
        }

        private void OnSurrenderClick()
        {
            var message = new NetSurrenderRequest();
            networkClient.SendServer(message);
        }

        private void OnCellMarked(OnCellMarked message)
        {
            if (message.Outcome != MarkOutcome.None)
            {
                var isDraw = message.Outcome == MarkOutcome.Draw;
                ShowEndGamePopup(message.Actor, isDraw).Forget();
                return;
            }

            turnUI.SetTurn(gameManager.IsMyTurn);
        }
        
        private void OnGameRestart(OnGameRestart message)
        {
            turnUI.SetTurn(gameManager.IsMyTurn);
        }

        private async UniTaskVoid ShowEndGamePopup(string winnerId, bool isDraw)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(showPopupDelay), cancellationToken: destroyCancellationToken);
            if(destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            endGamePopup.Show(winnerId, isDraw);
        }

        private void InitHeader()
        {
            var activeGame = gameManager.ActiveGame;
            xUserView.SetUser(activeGame.XUser, 0);
            oUserView.SetUser(activeGame.OUser, 0);
        }
    }
}