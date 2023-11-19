using TTC.Shared;
using TTC.Shared.Attributes;
using TTC.Shared.Handlers;

namespace TicTacServer.PacketHandlers;

[HandlerRegister(PacketType.FindOpponentRequest)]
public class FindOpponentRequestHandler : IPacketHandler
{
    public void Handle(INetPacket packet, int connectionId)
    {
        Console.WriteLine("Received FindOpponentRequest");
    }
}