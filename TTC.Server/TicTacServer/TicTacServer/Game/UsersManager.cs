using LiteNetLib;
using TicTacServer.Data;

namespace TicTacServer.Game;

public class UsersManager
{
    private Dictionary<int, ServerConnection> connetions;
    private readonly IUserRepository userRepository;

    public UsersManager(
        IUserRepository userRepository)
    {
        connetions = new Dictionary<int, ServerConnection>();
        this.userRepository = userRepository;
    }

    public void AddConnection(NetPeer peer)
    {
        connetions.Add(peer.Id, new ServerConnection
        {
            ConnectionId = peer.Id,
            Peer = peer,
        });
    }
    
    public bool LoginOrRegister(int connectionId, string username, string password)
    {
        var user = userRepository.Get(username);
        if (user != null)
        {
            if (user.Password != password)
            {
                return false;
            }
        }
        else
        {
            user = new User
            {
                Id = username,
                Password = password,
                IsOnline = true,
                Score = 0,
            };
            
            userRepository.Add(user);
        }

        if (connetions.TryGetValue(connectionId, out var connection))
        {
            user.IsOnline = true;
            connection.User = user;
        }
        
        return true;
    }

    public void Disconnect(int peerId)
    {
        var connection = GetConnection(peerId);
        if (connection.User != null)
        {
            var userId = connection.User.Id;
            userRepository.SetOffline(userId);
        }
        
        connetions.Remove(peerId);
    }

    public ServerConnection GetConnection(int peerId)
    {
        return connetions[peerId];
    }

    public int[] GetOtherConnectionIds(int excludeConnectionId)
    {
        return connetions.Keys
                         .Where(x => x != excludeConnectionId)
                         .ToArray();
    }
}