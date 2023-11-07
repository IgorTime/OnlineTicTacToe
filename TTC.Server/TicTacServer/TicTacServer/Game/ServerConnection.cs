using LiteNetLib;
using TicTacServer.Data;

namespace TicTacServer.Game;

public class ServerConnection
{
    public int ConnectionId { get; set; }
    public User User { get; set; }
    public NetPeer Peer { get; set; }
    public Guid? GameId { get; set; }
}