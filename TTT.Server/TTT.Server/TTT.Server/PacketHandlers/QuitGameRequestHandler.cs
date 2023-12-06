using Microsoft.Extensions.Logging;
using TTT.Server.GameLogic;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.PacketHandlers;

[HandlerRegister(PacketType.QuitGameRequest)]
public class QuitGameRequestHandler : PacketHandler<NetQuitGameRequest>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer networkServer;
    private readonly ILogger<QuitGameRequestHandler> logger;

    public QuitGameRequestHandler(
        UsersManager usersManager,
        GamesManager gamesManager,
        NetworkServer networkServer,
        ILogger<QuitGameRequestHandler> logger)
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
        this.networkServer = networkServer;
        this.logger = logger;
    }
    
    protected override void Handle(NetQuitGameRequest message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var userId = connection.User.Id;
        
        if (!gamesManager.GameExists(userId))
        {
            logger.LogWarning($"Player {userId} tried to quit a game, but they are not in one");
            return;
        }
        
        var closedGame = gamesManager.CloseGame(userId);
        var opponentId = closedGame.GetOpponentId(userId);
        var opponentConnection = usersManager.GetConnection(opponentId);

        var response = new NetOnQuitGame()
        {
            QuitterName = userId
        };
        
        networkServer.SendClient(connection.ConnectionId, response);
        networkServer.SendClient(opponentConnection.ConnectionId, response);
    }
}