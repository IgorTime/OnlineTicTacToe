using LiteNetLib.Utils;

namespace TTT.Shared.Handlers
{
    public interface IPacketHandler
    {
        void Handle(NetDataReader packetReader, int connectionId);
    }
}