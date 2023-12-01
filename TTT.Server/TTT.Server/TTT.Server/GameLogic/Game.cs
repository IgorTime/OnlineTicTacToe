using TTT.Shared.Models;

namespace TTT.Server.GameLogic;

public class Game
{
    private const int GRID_SIZE = 3;

    private readonly Grid grid;

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

    public Game(string xUser, string oUser)
    {
        Id = Guid.NewGuid();
        XUser = xUser;
        OUser = oUser;
        StartTime = DateTime.UtcNow;
        CurrentRoundStartTime = DateTime.UtcNow;
        Round = 1;
        CurrentUser = xUser;
        grid = new Grid(GRID_SIZE);
    }

    public MarkResult MarkCell(byte cellIndex)
    {
        var playerType = GetPlayerType(CurrentUser);
        grid.MarkCell(cellIndex, playerType);
        var (isWin, winLineType) = grid.CheckWin();

        var result = new MarkResult();
        if (isWin)
        {
            result.Outcome = MarkOutcome.Win;
            result.WinLineType = winLineType;
        }
        else
        {
            var draw = grid.CheckDraw();
            if (draw)
            {
                result.Outcome = MarkOutcome.Draw;
            }
        }

        return result;
    }

    public MarkType GetCell(byte cellIndex) => grid.GetCell(cellIndex);

    public string GetOpponent(string userId) => XUser == userId ? OUser : XUser;

    public void SwitchCurrentPlayer()
    {
        CurrentUser = GetOpponent(CurrentUser);
    }

    public void AddWin(string userId)
    {
        var winnerType = GetPlayerType(userId);
        if (winnerType == MarkType.X)
        {
            XWins++;
        }
        else
        {
            OWins++;
        }
    }

    private MarkType GetPlayerType(string userId) =>
        userId == XUser
            ? MarkType.X
            : MarkType.O;
}