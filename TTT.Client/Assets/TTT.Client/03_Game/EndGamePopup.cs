using TMPro;
using TriInspector;
using TTT.Client.Configs;
using TTT.Client.Gameplay;
using TTT.Shared.Models;
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

        [Header("Buttons:")]
        [SerializeField]
        private Button playAgainButton;

        [SerializeField]
        private Button acceptButton;

        [SerializeField]
        private Button quitButton;

        private IGameManager gameManager;

        [Inject]
        public void Construct(IGameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            InternalHide();
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            acceptButton.onClick.AddListener(OnAcceptClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        public void Show(string winnerId, bool isDraw)
        {
            var state = GetPopupState(winnerId, isDraw);
            var winnerMark  = isDraw ? MarkType.None : gameManager.ActiveGame.GetUserMark(winnerId);
            SetHeaderState(state, winnerMark);
            InternalShow();
        }

        private void OnQuitClicked()
        {
            throw new System.NotImplementedException();
        }

        private void OnAcceptClicked()
        {
            throw new System.NotImplementedException();
        }

        private void OnPlayAgainClicked()
        {
            throw new System.NotImplementedException();
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
            if(state == PopupState.Draw)
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