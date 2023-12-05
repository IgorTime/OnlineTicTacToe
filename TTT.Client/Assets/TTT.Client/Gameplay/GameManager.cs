using System;
using TTT.Client.User;
using TTT.Shared.Models;

namespace TTT.Client.Gameplay
{
    public class GameManager : IGameManager
    {
        private readonly IUserService userService;
        public string MyUsername { get; private set; }
        public string OpponentUsername { get; private set; }
        public MarkType MyMark { get; private set; }
        public MarkType OpponentMark { get; private set; }

        public Game ActiveGame { get; private set; }

        public bool InputEnabled { get; set; }

        public bool IsMyTurn
        {
            get
            {
                if (ActiveGame == null)
                {
                    return false;
                }

                return ActiveGame.CurrentUser == MyUsername;
            }
        }

        public GameManager(IUserService userService)
        {
            this.userService = userService;
        }

        public void RegisterGame(Guid gameId, string xUser, string oUser)
        {
            ActiveGame = new Game
            {
                GameId = gameId,
                XUser = xUser,
                OUser = oUser,
                CurrentUser = xUser,
                StartTime = DateTime.Now,
            };

            MyUsername = userService.UserName;
            OpponentUsername = MyUsername == xUser ? oUser : xUser;
            MyMark = MyUsername == xUser ? MarkType.X : MarkType.O;
            OpponentMark = MyMark == MarkType.X ? MarkType.O : MarkType.X;

            InputEnabled = true;
        }

        public void Reset()
        {
            ActiveGame.Reset();
        }
    }
}