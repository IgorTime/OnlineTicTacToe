using System;
using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Shared.Models;
using UnityEngine;
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

        private IGameManager gameManager;
        private IDisposable unSubscriber;

        [Inject]
        public void Construct(
            IGameManager gameManager,
            ISubscriber<OnCellMarked> onCellMarked)
        {
            this.gameManager = gameManager;
            unSubscriber = onCellMarked.Subscribe(OnCellMarked);
        }

        private void Start()
        {
            InitHeader();
            turnUI.SetTurn(gameManager.IsMyTurn);
        }

        private void OnDestroy()
        {
            unSubscriber?.Dispose();
        }

        private void OnCellMarked(OnCellMarked message)
        {
            if (message.Outcome != MarkOutcome.None)
            {
                var isDraw = message.Outcome == MarkOutcome.Draw;
                Debug.Log("Game over! " + (isDraw ? "Draw!" : message.Actor + " won!"));
                return;
            }

            turnUI.SetTurn(gameManager.IsMyTurn);
        }

        private void InitHeader()
        {
            var activeGame = gameManager.ActiveGame;
            xUserView.SetUser(activeGame.XUser, 0);
            oUserView.SetUser(activeGame.OUser, 0);
        }
    }
}