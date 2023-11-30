using System;
using TTT.Shared.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Client.Game
{
    public class CellView : MonoBehaviour
    {
        [SerializeField]
        private GameObject xMark;

        [SerializeField]
        private GameObject oMark;

        [SerializeField]
        private Button button;

        private Action<int> clickCallback;

        public int Index { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        private void Start()
        {
            button.onClick.AddListener(OnCellClicked);
        }

        public void SetMark(MarkType markType)
        {
            xMark.SetActive(markType == MarkType.X);
            oMark.SetActive(markType == MarkType.O);
        }

        public void Init(int index, int row, int column, Action<int> onCellClicked)
        {
            Index = index;
            Row = row;
            Column = column;
            clickCallback = onCellClicked;
            SetMark(MarkType.None);
        }

        private void OnCellClicked()
        {
            clickCallback?.Invoke(Index);
        }
    }
}