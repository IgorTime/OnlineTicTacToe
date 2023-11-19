using TTT.Shared;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.CancelFindOpponentRequest)]
public class CancelFindOpponentRequestHandler : PacketHandler<NetCancelFindOpponentRequest>
{
    protected override void Handle(NetCancelFindOpponentRequest packet, int connectionId)
    {
        Console.WriteLine("Received Cancel");
    }
}