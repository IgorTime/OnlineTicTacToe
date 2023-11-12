using Microsoft.Extensions.Logging;
using TicTacServer.Data;
using TicTacServer.Game;
using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ClientServer;
using TTC.Shared.Packets.ServerClient;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.AuthRequest)]
public class AuthRequestHandler : IPacketHandler
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

    public void Handle(INetPacket packet, int connectionId)
    {
        var message = (NetAuthRequest) packet;
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
        var responseMessage = new NetOnServerStatus()
        {
            PlayersCount = userRepository.GetTotalCount(),
            TopPlayers = usersManager.GetTopPlayers(),
        };

        var otherIds = usersManager.GetOtherConnectionIds(excludeConnectionId);
        foreach (var id in otherIds)
        {
            server.SendClient(id, responseMessage);
        }
    }
}