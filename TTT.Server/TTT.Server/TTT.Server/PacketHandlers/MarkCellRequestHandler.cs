using Microsoft.Extensions.Logging;
using TTT.Server.GameLogic;
using TTT.Shared.Handlers;
using TTT.Shared.Models;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.PacketHandlers;

public class MarkCellRequestHandler : PacketHandler<NetMarkCellRequest>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer networkServer;
    private readonly ILogger<MarkCellRequestHandler> logger;

    public MarkCellRequestHandler(
        UsersManager usersManager,
        GamesManager gamesManager,
        NetworkServer networkServer,
        ILogger<MarkCellRequestHandler> logger) 
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
        this.networkServer = networkServer;
        this.logger = logger;
    }

    protected override void Handle(NetMarkCellRequest message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var userId = connection.User.Id;
        var game = gamesManager.FindGame(userId);

        ValidateAndThrow(message.Index, userId, game);
        
        var result = game.MarkCell(message.Index);
        var response = new NetOnMarkCell()
        {
            Actor = userId,
            Index = message.Index,
            Outcome = result.Outcome,
            WinLine = result.WinLineType
        };
        
        var opponentId = game.GetOpponent(userId);
        var opponentConnection = usersManager.GetConnection(opponentId);
        
        networkServer.SendClient(connection.ConnectionId, response);
        networkServer.SendClient(opponentConnection.ConnectionId, response);
        logger.LogInformation($"'{userId}' marked cell '{message.Index}' with outcome '{result.Outcome}'");

        if (result.Outcome == MarkOutcome.None)
        {
            game.SwitchCurrentPlayer();
            return;
        }

        if (result.Outcome == MarkOutcome.Win)
        {
            game.AddWin(userId);
            usersManager.IncreaseScore(userId);
            logger.LogInformation($"'{userId}' won the game! Increasing score and win counter.");
        }
    }

    private static void ValidateAndThrow(byte cellIndex, string actor, Game game)
    {
        if (game.CurrentUser != actor)
        {
            throw new ArgumentException("[Bad Request] Actor '{actor}' is not the current user!");
        }
        
        if(game.GetCell(cellIndex) != MarkType.None)
        {
            throw new ArgumentException($"[Bad Request] Cell with index '{cellIndex}' is already marked!");
        }
    }
}