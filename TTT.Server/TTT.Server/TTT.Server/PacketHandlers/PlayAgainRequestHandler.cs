using TTT.Server.GameLogic;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.PacketHandlers;

[HandlerRegister(PacketType.PlayAgainRequest)]
public class PlayAgainRequestHandler : PacketHandler<NetPlayAgainRequest>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer networkServer;

    public PlayAgainRequestHandler(
        UsersManager usersManager,
        GamesManager gamesManager,
        NetworkServer networkServer) 
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
        this.networkServer = networkServer;
    }

    protected override void Handle(NetPlayAgainRequest message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var userId = connection.User.Id;
        var game = gamesManager.FindGame(userId);
        game.SetReadyToPlayAgain(userId);

        var opponentId = game.GetOpponentId(userId);
        var opponentConnection = usersManager.GetConnection(opponentId);
        
        var response = new NetOnPlayAgain();
        networkServer.SendClient(opponentConnection.ConnectionId, response);
    }
}