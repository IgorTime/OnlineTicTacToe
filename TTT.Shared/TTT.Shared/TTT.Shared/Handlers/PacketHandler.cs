using LiteNetLib.Utils;

namespace TTT.Shared.Handlers
{
    public abstract class PacketHandler<T> : IPacketHandler
        where T : struct, INetPacket
    {
        public void Handle(NetDataReader packetReader, int connectionId)
        {
            var packet = new T();
            packet.Deserialize(packetReader);
            Handle(packet, connectionId);
        }

        protected abstract void Handle(T message, int connectionId);
    }
}