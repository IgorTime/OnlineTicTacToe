using System;
using TTT.Shared.Models;

namespace TTT.Client.Gameplay
{
    public interface IGameManager
    {
        string MyUsername { get; }
        string OpponentUsername { get; }
        public MarkType MyMark { get; }
        public MarkType OpponentMark { get; }
        
        Game ActiveGame { get; }
        bool InputsEnable { get; }
        bool IsMyTurn { get; }
        void RegisterGame(Guid gameId, string xUser, string oUser);
    }
}