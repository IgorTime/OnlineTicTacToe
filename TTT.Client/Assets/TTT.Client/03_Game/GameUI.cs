﻿using TTT.Client.Gameplay;
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

        [Inject]
        public void Construct(IGameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            InitHeader();
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