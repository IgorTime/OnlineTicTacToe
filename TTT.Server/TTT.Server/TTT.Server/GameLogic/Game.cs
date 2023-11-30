using TTT.Server.Utilities;
using TTT.Shared.Models;

namespace TTT.Server.GameLogic;

public class Game
{
    private const int GRID_SIZE = 3;

    public Guid Id { get; set; }
    public ushort Round { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime CurrentRoundStartTime { get; set; }
    public string OUser { get; set; }
    public string XUser { get; set; }
    public bool OWantsRematch { get; set; }
    public bool XWantsRematch { get; set; }
    public ushort OWins { get; set; }
    public ushort XWins { get; set; }
    public string CurrentUser { get; set; }

    public MarkType[,] Grid { get; }

    public Game(string xUser, string oUser)
    {
        Id = Guid.NewGuid();
        XUser = xUser;
        OUser = oUser;
        StartTime = DateTime.UtcNow;
        CurrentRoundStartTime = DateTime.UtcNow;
        Round = 1;
        CurrentUser = xUser;
        Grid = new MarkType[GRID_SIZE, GRID_SIZE];
    }

    public MarkResult MarkCell(byte cellIndex)
    {
        var playerType = GetPlayerType(CurrentUser);
        var (row, column) = GridExtensions.GetCoordinates(cellIndex, GRID_SIZE);
        Grid[row, column] = playerType;
        var (isWin, winLineType) = Grid.CheckWin(GRID_SIZE);

        var result = new MarkResult();
        if (isWin)
        {
            result.Outcome = MarkOutcome.Win;
            result.WinLineType = winLineType;
        }
        else
        {
            var draw = Grid.CheckDraw(GRID_SIZE);
            if (draw)
            {
                result.Outcome = MarkOutcome.Draw;
            }
        }

        return result;
    }

    public MarkType GetCell(byte cellIndex) => Grid.GetCell(cellIndex, GRID_SIZE);

    public string GetOpponent(string userId) => XUser == userId ? OUser : XUser;

    private MarkType GetPlayerType(string userId) =>
        userId == XUser
            ? MarkType.X
            : MarkType.O;
}