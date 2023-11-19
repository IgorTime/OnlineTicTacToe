using TTT.Shared;
using TTT.Shared.Handlers;

namespace TTT.Client.PacketHandlers
{
    public interface IPacketHandlerResolver
    {
        IPacketHandler Resolve(PacketType packetType);
    }
}