using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ClientServer;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.FindOpponentRequest)]
public class FindOpponentRequestHandler : PacketHandler<NetFindOpponentRequest>
{
    protected override void Handle(NetFindOpponentRequest packet, int connectionId)
    {
        Console.WriteLine("Received FindOpponentRequest");
    }
}