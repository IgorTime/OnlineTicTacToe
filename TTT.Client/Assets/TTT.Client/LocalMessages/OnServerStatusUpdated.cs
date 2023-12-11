using TTT.Shared.Packets.ServerClient;

namespace TTT.Client.LocalMessages
{
    public struct OnServerStatusUpdated
    {
        public ushort PlayersCount;
        public PlayerNetDto[] TopPlayers;
    }
}