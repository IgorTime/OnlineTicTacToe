using System;
using DG.Tweening;
using MessagePipe;
using TMPro;
using TriInspector;
using TTT.Client.Configs;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Shared.Models;
using TTT.Shared.Packets.ClientServer;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TTT.Client.Game
{
    public class EndGamePopup : MonoBehaviour
    {
        private enum PopupState : byte
        {
            Win = 0,
            Loose = 1,
            Draw = 2,
            WaitForOpponent = 3,
            WantsToPlayAgain = 4,
            OpponentLeft = 5,
        }

        [SerializeField]
        private PlayerColorsConfig playerColors;

        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private RectTransform background;

        [Header("Header:")]
        [SerializeField]
        private Image headerBackground;

        [SerializeField]
        private TextMeshProUGUI youWinText;

        [SerializeField]
        private TextMeshProUGUI youLooseText;

        [SerializeField]
        private TextMeshProUGUI drawText;

        [SerializeField]
        private TextMeshProUGUI waitForOpponentText;

        [SerializeField]
        private TextMeshProUGUI opponentLeftText;

        [SerializeField]
        private TextMeshProUGUI wantsToPlayAgainText;

        [Header("Buttons:")]
        [SerializeField]
        private Button playAgainButton;

        [SerializeField]
        private Button acceptButton;

        [SerializeField]
        private Button quitButton;

        private IGameManager gameManager;
        private INetworkClient networkClient;
        private IDisposable unSubscriber;

        [Inject]
        public void Construct(
            IGameManager gameManager,
            INetworkClient networkClient,
            ISubscriber<OnPlayAgain> onPlayAgain)
        {
            this.gameManager = gameManager;
            this.networkClient = networkClient;

            unSubscriber = onPlayAgain.Subscribe(OnPlayAgainMessageReceived);
        }

        private void Start()
        {
            InternalHide();
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            acceptButton.onClick.AddListener(OnAcceptClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnDestroy()
        {
            playAgainButton.onClick.RemoveListener(OnPlayAgainClicked);
            acceptButton.onClick.RemoveListener(OnAcceptClicked);
            quitButton.onClick.RemoveListener(OnQuitClicked);
            unSubscriber?.Dispose();
        }

        public void Show(string winnerId, bool isDraw)
        {
            var state = GetPopupState(winnerId, isDraw);
            var winnerMark = isDraw ? MarkType.None : gameManager.ActiveGame.GetUserMark(winnerId);
            SetHeaderState(state, winnerMark);
            SetButtonsState(state);
            SetTextState(state);
            InternalShow();
        }

        private void SetButtonsState(PopupState state)
        {
            playAgainButton.gameObject.SetActive(state is PopupState.Win or PopupState.Loose or PopupState.Draw);
            acceptButton.gameObject.SetActive(state == PopupState.WantsToPlayAgain);
            quitButton.gameObject.SetActive(true);
        }

        private void SetTextState(PopupState state)
        {
            waitForOpponentText.gameObject.SetActive(state == PopupState.WaitForOpponent);
            wantsToPlayAgainText.gameObject.SetActive(state == PopupState.WantsToPlayAgain);
            opponentLeftText.gameObject.SetActive(state == PopupState.OpponentLeft);
        }

        private void OnPlayAgainMessageReceived(OnPlayAgain message)
        {
            playAgainButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(true);
        }

        private void OnQuitClicked()
        {
        }

        private void OnAcceptClicked()
        {
        }

        private void OnPlayAgainClicked()
        {
            var message = new NetPlayAgainRequest();
            networkClient.SendServer(message);

            SetButtonsState(PopupState.WaitForOpponent);
            SetTextState(PopupState.WaitForOpponent);
        }

        private PopupState GetPopupState(string winnerId, bool isDraw)
        {
            if (isDraw)
            {
                return PopupState.Draw;
            }

            return gameManager.MyUsername == winnerId
                ? PopupState.Win
                : PopupState.Loose;
        }

        private void SetHeaderState(PopupState state, MarkType winnerMark = MarkType.None)
        {
            youWinText.gameObject.SetActive(state == PopupState.Win);
            youLooseText.gameObject.SetActive(state == PopupState.Loose);
            drawText.gameObject.SetActive(state == PopupState.Draw);

            headerBackground.color = GetHeaderColor(state, winnerMark);
        }

        private Color GetHeaderColor(PopupState state, MarkType winnerMark)
        {
            if (state == PopupState.Draw)
            {
                return headerBackground.color;
            }

            return state == PopupState.Loose
                ? Color.red
                : winnerMark == MarkType.X
                    ? playerColors.XPlayerColor
                    : playerColors.OPlayerColor;
        }

        private void InternalShow()
        {
            content.gameObject.SetActive(true);
            background.gameObject.SetActive(true);

            content.localScale = Vector3.one * 0.8f;
            content.DOScale(1f, 0.2f)
                   .SetEase(Ease.OutBack)
                   .SetLink(gameObject);
        }

        private void InternalHide()
        {
            content.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
        }

        [Button]
        private void SetStateDebug(PopupState state, MarkType mark)
        {
            if(state is PopupState.Win or PopupState.Loose or PopupState.Draw)
            {
                SetHeaderState(state, mark);
            }
            
            SetButtonsState(state);
            SetTextState(state);
        }
    }
}