using TTT.Server.GameLogic;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;

namespace TTT.Server.PacketHandlers;

public class MarkCellRequestHandler : PacketHandler<NetMarkCellRequest>
{
    private readonly UsersManager usersManager;
    private readonly GamesManager gamesManager;

    public MarkCellRequestHandler(
        UsersManager usersManager, 
        GamesManager gamesManager)
    {
        this.usersManager = usersManager;
        this.gamesManager = gamesManager;
    }
    
    protected override void Handle(NetMarkCellRequest message, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        var userId = connection.User.Id;
        var game = gamesManager.FindGame(userId);

        Validate(message.Index, userId, game);

        // 1. Validate the message
        // 2. Get the game and invoke MarkCell
    }

    private void Validate(byte cellIndex, string userId, Game game)
    {
    }
}