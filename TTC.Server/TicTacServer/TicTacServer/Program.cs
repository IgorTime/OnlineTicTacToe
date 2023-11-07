using Microsoft.Extensions.DependencyInjection;
using TicTacServer;
using TicTacServer.Infrastructure;

var serviceProvider = Container.Configure();
var server = serviceProvider.GetRequiredService<NetworkServer>();
server.Start();

while (true)
{
    server.PollEvents();
    Thread.Sleep(15);
}