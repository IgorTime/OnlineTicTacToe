using TicTacServer.Data;
using TicTacServer.Game;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.ServerStatusRequest)]
public class ServerStatusRequestHandler : PacketHandler<NetServerStatusRequest>
{
    private readonly NetworkServer server;
    private readonly IUserRepository userRepository;
    private readonly UsersManager usersManager;

    public ServerStatusRequestHandler(
        NetworkServer server,
        IUserRepository userRepository,
        UsersManager usersManager)
    {
        this.server = server;
        this.userRepository = userRepository;
        this.usersManager = usersManager;
    }

    protected override void Handle(NetServerStatusRequest packet, int connectionId)
    {
        var msg = new NetOnServerStatus
        {
            PlayersCount = userRepository.GetTotalCount(),
            TopPlayers = usersManager.GetTopPlayers(),
        };

        server.SendClient(connectionId, msg);
    }
}