using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicTacServer.Data;
using TicTacServer.Extensions;
using TicTacServer.Game;
using TicTacServer.Matchmaking;
using TTT.Shared.Registries;

namespace TicTacServer.Infrastructure;

public static class Container
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        return services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(c => c.AddSimpleConsole());
        services.AddSingleton<NetworkServer>();
        services.AddSingleton<PacketHandlerRegistry>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddSingleton<UsersManager>();
        services.AddSingleton<Matchmaker>();
        services.AddSingleton<GamesManager>();
        services.AddPacketHandlers();
    }
}