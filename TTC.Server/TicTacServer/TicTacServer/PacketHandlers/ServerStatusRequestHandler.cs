using TicTacServer.Data;
using TicTacServer.Game;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ServerClient;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.ServerStatusRequest)]
public class ServerStatusRequestHandler : IPacketHandler
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
                                          
    public void Handle(INetPacket packet, int connectionId)
    {
        var msg = new NetOnServerStatus()
        {
            PlayersCount = userRepository.GetTotalCount(),
            TopPlayers = usersManager.GetTopPlayers(),
        };
        
        server.SendClient(connectionId, msg);
    }
}