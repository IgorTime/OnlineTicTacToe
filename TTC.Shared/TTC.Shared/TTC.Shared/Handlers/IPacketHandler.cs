using LiteNetLib.Utils;

namespace TTC.Shared.Handlers
{
    public interface IPacketHandler
    {
        void Handle(NetDataReader packetReader, int connectionId);
    }
}