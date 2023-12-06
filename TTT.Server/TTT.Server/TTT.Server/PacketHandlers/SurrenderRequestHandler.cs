using TTT.Server.GameLogic;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.PacketHandlers;

[HandlerRegister(PacketType.SurrenderRequest)]
public class SurrenderRequestHandler : PacketHandler<NetOnSurrender>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;
    private readonly NetworkServer networkServer;

    public SurrenderRequestHandler(
        UsersManager usersManager,
        GamesManager gamesManager,
        NetworkServer networkServer)
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
        this.networkServer = networkServer;
    }
    
    protected override void Handle(NetOnSurrender message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var game = gamesManager.FindGame(connection.User.Id);
        var opponentId = game.GetOpponentId(connection.User.Id);
        var opponentConnectionId = usersManager.GetConnection(opponentId).ConnectionId;
        
        game.AddWin(opponentId);
        usersManager.IncreaseScore(opponentId);

        var response = new NetOnSurrender()
        {
            WinnerName = opponentId
        };
        
        networkServer.SendClient(connectionId, response);
        networkServer.SendClient(opponentConnectionId, response);
    }
}