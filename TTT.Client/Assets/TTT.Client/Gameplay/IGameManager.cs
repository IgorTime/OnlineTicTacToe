using System;

namespace TTT.Client.Gameplay
{
    public interface IGameManager
    {
        Game ActiveGame { get; }
        bool InputsEnable { get; }
        void RegisterGame(Guid gameId, string xUser, string oUser);
    }
}