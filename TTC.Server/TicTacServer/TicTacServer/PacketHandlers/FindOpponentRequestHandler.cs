using TicTacServer.Game;
using TicTacServer.Matchmaking;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ClientServer;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.FindOpponentRequest)]
public class FindOpponentRequestHandler : PacketHandler<NetFindOpponentRequest>
{
    private readonly UsersManager usersManager;
    private readonly Matchmaker matchmaker;

    public FindOpponentRequestHandler(
        UsersManager usersManager,
        Matchmaker matchmaker)
    {
        this.usersManager = usersManager;
        this.matchmaker = matchmaker;
    }
    
    protected override void Handle(NetFindOpponentRequest packet, int connectionId)
    {
        var connection = usersManager.GetConnection(connectionId);
        matchmaker.RegisterPlayer(connection);
    }
}