using Microsoft.Extensions.Logging;
using TicTacServer.Game;

namespace TicTacServer.Matchmaking;

public class Matchmaker
{
    private readonly ILogger<Matchmaker> logger;
    private List<MatchmakingRequest> pool = new();

    public Matchmaker(
        ILogger<Matchmaker> logger)
    {
        this.logger = logger;
    }
    
    public void RegisterPlayer(ServerConnection connection)
    {
        if (pool.Any(x => x.Connection.User.Id == connection.User.Id))
        {
            logger.LogWarning($"{connection.User.Id} is already registered. Ignoring...");
            return;
        }
        
        pool.Add(new MatchmakingRequest
        {
            Connection = connection,
            SearchStartedAt = DateTime.UtcNow,
        });
        
        logger.LogInformation($"{connection.User.Id} registered for matchmaking");

        DoMatchmaking();
    }

    public void TryUnregisterPlayer(string username)
    {
        var request = pool.FirstOrDefault(x => x.Connection.User.Id == username);
        if (request != null)
        {
            logger.LogInformation($"Removing {username} from matchmaking pool");
            pool.Remove(request);
        }
    }

    private void DoMatchmaking()
    {
        logger.LogInformation("Doing matchmaking...");
    }
}