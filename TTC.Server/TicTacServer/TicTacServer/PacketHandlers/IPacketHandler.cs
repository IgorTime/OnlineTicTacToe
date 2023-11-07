using SharedLib;

namespace TicTacServer.PacketHandlers;

public interface IPacketHandler
{
    void Handle(INetPacket packet, int connectionId);
}