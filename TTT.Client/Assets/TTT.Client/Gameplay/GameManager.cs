using System;

namespace TTT.Client.Gameplay
{
    public class GameManager : IGameManager
    {
        public Game ActiveGame { get; private set; }

        public bool InputsEnable { get; private set; }

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

            InputsEnable = true;
        }
    }
}