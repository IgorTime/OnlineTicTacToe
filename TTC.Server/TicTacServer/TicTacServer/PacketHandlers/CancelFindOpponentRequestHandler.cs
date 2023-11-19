using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;
using TTC.Shared.Packets.ClientServer;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.CancelFindOpponentRequest)]
public class CancelFindOpponentRequestHandler : PacketHandler<NetCancelFindOpponentRequest>
{
    protected override void Handle(NetCancelFindOpponentRequest packet, int connectionId)
    {
        Console.WriteLine("Received Cancel");
    }
}