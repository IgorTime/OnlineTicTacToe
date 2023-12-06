﻿namespace TTT.Shared
{
    public enum PacketType : byte
    {
        #region Client to Server

        Invalid = 0,
        AuthRequest = 1,
        ServerStatusRequest = 2,
        FindOpponentRequest = 3,
        CancelFindOpponentRequest = 4,
        MarkCellRequest = 5,
        PlayAgainRequest = 6,
        AcceptPlayAgainRequest = 7,
        SurrenderRequest = 8,
        QuitGameRequest = 9,
        #endregion

        #region Server to Client

        OnAuth = 100,
        OnAuthFail = 101,
        OnServerStatus = 102,
        OnFindOpponent = 103,
        OnStartGame = 104,
        OnMarkCell = 105,
        OnPlayAgain = 106,
        OnNewRound = 107,
        OnSurrender = 108,
        OnQuitGame = 109,
        #endregion
    }
}