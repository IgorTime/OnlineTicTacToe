using TTT.Server.Game;
using TTT.Server.Matchmaking;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;

namespace TTT.Server.PacketHandlers;

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