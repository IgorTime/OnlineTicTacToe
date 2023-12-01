using TTT.Shared.Handlers;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.PacketHandlers
{
    public class OnMarkCellHandler : PacketHandler<NetOnMarkCell>
    {
        protected override void Handle(NetOnMarkCell message, int connectionId)
        {
            
        }
    }
}