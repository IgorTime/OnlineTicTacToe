namespace TTT.Server.Game;

public class Game
{
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
    }
}