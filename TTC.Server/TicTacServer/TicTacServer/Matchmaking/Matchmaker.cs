using Microsoft.Extensions.Logging;
using TicTacServer.Game;

namespace TicTacServer.Matchmaking;

public class Matchmaker
{
    private readonly ILogger<Matchmaker> logger;
    private readonly GamesManager gamesManager;
    private readonly List<MatchmakingRequest> pool = new();

    public Matchmaker(
        ILogger<Matchmaker> logger,
        GamesManager gamesManager)
    {
        this.logger = logger;
        this.gamesManager = gamesManager;
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

        for (var i = 0; i < pool.Count; i++)
        {
            var request = pool[i];
            var match = pool.FirstOrDefault(x => !x.MatchFound && x.Connection.User.Id != pool[i].Connection.User.Id);
            if (match == null)
            {
                continue;
            }
            
            match.MatchFound = true;
            request.MatchFound = true;
            pool.Remove(match);
            pool.Remove(request);
            i -= 2;

            var xUser = request.Connection.User.Id;
            var oUser = match.Connection.User.Id;
            var gameId = gamesManager.RegisterGame(xUser, oUser);
            request.Connection.GameId = gameId;
            match.Connection.GameId = gameId;
            // Send message OnGameStart
            
            logger.LogInformation($"Match found: {xUser}(X) vs {oUser}(O)");
        }
    }
}