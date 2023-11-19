using Microsoft.Extensions.Logging;
using TicTacServer.Data;
using TicTacServer.Game;
using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.AuthRequest)]
public class AuthRequestHandler : PacketHandler<NetAuthRequest>
{
    private readonly ILogger<AuthRequestHandler> logger;
    private readonly IUserRepository userRepository;
    private readonly UsersManager usersManager;
    private readonly NetworkServer server;

    public AuthRequestHandler(
        ILogger<AuthRequestHandler> logger,
        IUserRepository userRepository,
        UsersManager usersManager,
        NetworkServer server)
    {
        this.logger = logger;
        this.userRepository = userRepository;
        this.usersManager = usersManager;
        this.server = server;
    }

    protected override void Handle(NetAuthRequest message, int connectionId)
    {
        logger.LogInformation($"Received auth request from {message.Username}" +
                              $"with password: {message.Password}");

        var loginSuccess = usersManager.LoginOrRegister(
            connectionId,
            message.Username,
            message.Password);

        if (loginSuccess)
        {
            server.SendClient(connectionId, new NetOnAuth());
            NotifyOtherPlayers(connectionId);
        }
        else
        {
            server.SendClient(connectionId, new NetOnAuthFail());
        }
    }

    private void NotifyOtherPlayers(int excludeConnectionId)
    {
        server.NotifyOtherPlayers(
            userRepository.GetTotalCount(),
            usersManager.GetTopPlayers(),
            usersManager.GetOtherConnectionIds(excludeConnectionId));
    }
}