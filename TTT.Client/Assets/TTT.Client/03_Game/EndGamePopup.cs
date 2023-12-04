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
            InternalShow();
        }

        private void OnPlayAgainMessageReceived(OnPlayAgain message)
        {
            playAgainButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(true);
            
            waitForOpponentText.gameObject.SetActive(false);
            wantsToPlayAgainText.gameObject.SetActive(true);
            opponentLeftText.gameObject.SetActive(false);
        }

        private void OnQuitClicked()
        {
            throw new NotImplementedException();
        }

        private void OnAcceptClicked()
        {
            throw new NotImplementedException();
        }

        private void OnPlayAgainClicked()
        {
            playAgainButton.gameObject.SetActive(false);
            waitForOpponentText.gameObject.SetActive(true);
            wantsToPlayAgainText.gameObject.SetActive(false);
            opponentLeftText.gameObject.SetActive(false);

            var message = new NetPlayAgainRequest();
            networkClient.SendServer(message);
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
            SetHeaderState(state, mark);
        }
    }
}