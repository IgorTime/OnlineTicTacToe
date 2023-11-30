using TTT.Client.Gameplay;
using TTT.Shared.Packets.ClientServer;
using UnityEngine;
using VContainer;

namespace TTT.Client.Game
{
    public class BoardUI : MonoBehaviour
    {
        public const byte BOARD_SIZE = 3;
        
        [SerializeField]
        private CellView[] cells;

        private IGameManager gameManager;
        private INetworkClient networkClient;

        [Inject]
        public void Construct(
            IGameManager gameManager,
            INetworkClient networkClient)
        {
            this.gameManager = gameManager;
            this.networkClient = networkClient;
        }
        
        private void Start()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            for (var i = 0; i < cells.Length; i++)
            {
                var row = i / BOARD_SIZE;
                var column = i % BOARD_SIZE;
                cells[i].Init(i, row, column, OnCellClicked);
            }
        }

        private void OnCellClicked(int cellIndex)
        {
            if (!gameManager.InputEnabled)
            {
                return;
            }

            if (!gameManager.IsMyTurn)
            {
                Debug.Log("Not my turn");
            }
            else
            {
                Debug.Log($"Cell {cellIndex} clicked");
                gameManager.InputEnabled = false;

                var msg = new NetMarkCellRequest()
                {
                    Index = (byte)cellIndex
                };
                networkClient.SendServer(msg);
            }
        }

        private void Reset()
        {
            cells = GetComponentsInChildren<CellView>();
        }
    }
}