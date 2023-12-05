using Microsoft.Extensions.Logging;
using TTT.Server.GameLogic;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.PacketHandlers;

[HandlerRegister(PacketType.AcceptPlayAgainRequest)]
public class AcceptPlayAgainRequestHandler : PacketHandler<NetAcceptAgainRequest>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer networkServer;
    private readonly ILogger<AcceptPlayAgainRequestHandler> logger;

    public AcceptPlayAgainRequestHandler(
        UsersManager usersManager,
        GamesManager gamesManager,
        NetworkServer networkServer,
        ILogger<AcceptPlayAgainRequestHandler> logger)
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
        this.networkServer = networkServer;
        this.logger = logger;
    }

    protected override void Handle(NetAcceptAgainRequest message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var userId = connection.User.Id;
        var game = gamesManager.FindGame(userId);
        game.SetReadyToPlayAgain(userId);

        if (!game.BothPlayersReadyToRematch())
        {
            logger.LogWarning("Bad state! Both players should be ready to play again!");
            return;
        }

        game.NewRound();

        var opponentId = game.GetOpponentId(userId);
        var opponentConnectionId = usersManager.GetConnection(opponentId).ConnectionId;
        
        var response = new NetOnNewRound();
        networkServer.SendClient(connectionId, response);
        networkServer.SendClient(opponentConnectionId, response);
    }
}