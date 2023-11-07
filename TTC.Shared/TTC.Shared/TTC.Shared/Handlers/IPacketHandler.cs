namespace TTC.Shared.Handlers
{
    public interface IPacketHandler
    {
        void Handle(INetPacket packet, int connectionId);
    }
}