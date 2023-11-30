using TTT.Shared.Handlers;
using TTT.Shared.Packets.ClientServer;

namespace TTT.Server.PacketHandlers;

public class MarkCellRequestHandler : PacketHandler<NetMarkCellRequest>
{
    protected override void Handle(NetMarkCellRequest message, int connectionId)
    {
        // 1. Validate the message
        // 2. Get the game and invoke MarkCell
    }
}