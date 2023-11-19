using TTT.Shared.Packets.ServerClient;

namespace TicTacServer;

public static class NetworkServerExtensions
{
    public static void NotifyOtherPlayers(
        this NetworkServer server, 
        ushort totalCount,
        PlayerNetDto[] topPlayers,
        int[] otherIds)
    {
        var responseMessage = new NetOnServerStatus()
        {
            PlayersCount = totalCount,
            TopPlayers = topPlayers,
        };
        
        foreach (var id in otherIds)
        {
            server.SendClient(id, responseMessage);
        }
    }
}