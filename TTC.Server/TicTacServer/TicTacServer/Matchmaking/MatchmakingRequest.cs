using TicTacServer.Game;

namespace TicTacServer.Matchmaking;

public class MatchmakingRequest
{
    public ServerConnection Connection { get; set; }
    public DateTime SearchStartedAt { get; set; }
    public bool MatchFound { get; set; }
}