using Microsoft.Extensions.Logging;
using TicTacServer.Game;
using TTT.Shared.Packets.ServerClient;

namespace TicTacServer.Matchmaking;

public class Matchmaker
{
    private readonly ILogger<Matchmaker> logger;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer server;
    private readonly List<MatchmakingRequest> pool = new();

    public Matchmaker(
        ILogger<Matchmaker> logger,
        GamesManager gamesManager,
        NetworkServer server)
    {
        this.logger = logger;
        this.gamesManager = gamesManager;
        this.server = server;
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

        var matchedPlayers = new List<MatchmakingRequest>();
        foreach (var request in pool)
        {
            var match = pool.FirstOrDefault(x => !x.MatchFound && x.Connection.User.Id != request.Connection.User.Id);
            if (match == null)
            {
                continue;
            }
            
            match.MatchFound = true;
            request.MatchFound = true;
            matchedPlayers.Add(match);
            matchedPlayers.Add(request);

            var xUser = request.Connection.User.Id;
            var oUser = match.Connection.User.Id;
            var gameId = gamesManager.RegisterGame(xUser, oUser);
            request.Connection.GameId = gameId;
            match.Connection.GameId = gameId;

            var msg = new NetOnStartGame()
            {
                XUser = xUser,
                OUser = oUser,
                GameId = gameId,
            };

            server.SendClient(request.Connection.ConnectionId, msg);
            server.SendClient(match.Connection.ConnectionId, msg);
            
            logger.LogInformation($"Match found: {xUser}(X) vs {oUser}(O)");
        }

        foreach (var matchedPlayer in matchedPlayers)
        {
            pool.Remove(matchedPlayer);
        }
    }
}