using Microsoft.Extensions.Logging;
using SharedLib;
using SharedLib.Packets.ClientServer;
using TicTacServer.Game;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.AuthRequest)]
public class AuthRequestHandler : IPacketHandler
{
    private readonly ILogger<AuthRequestHandler> logger;
    private readonly UsersManager usersManager;
    private readonly NetworkServer server;

    public AuthRequestHandler(
        ILogger<AuthRequestHandler> logger,
        UsersManager usersManager,
        NetworkServer server)
    {
        this.logger = logger;
        this.usersManager = usersManager;
        this.server = server;
    }
    
    public void Handle(INetPacket packet, int connectionId)
    {
        var message = (Net_AuthRequest) packet;
        logger.LogInformation($"Received auth request from {message.Username}" +
                              $"with password: {message.Password}");

        var loginSuccess = usersManager.LoginOrRegister(
            connectionId, 
            message.Username,
            message.Password);

        if (loginSuccess)
        {
            server.SendClient(connectionId, new Net_OnAuth());
            NotifyOtherPlayers(connectionId);
        }
        else
        {
            server.SendClient(connectionId, new Net_OnAuthFail());
        }

    }

    private void NotifyOtherPlayers(int excludeConnectionId)
    {
        var responseMessage = new Net_OnServerStatus()
        {

        };
        
        var otherIds = usersManager.GetOtherConnectionIds(excludeConnectionId);
        foreach (var id in otherIds)
        {
            server.SendClient(id, responseMessage);
        }
    }
}