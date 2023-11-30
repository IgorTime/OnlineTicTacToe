using LiteNetLib;
using TTT.Server.Data;
using TTT.Shared.Packets.ServerClient;

namespace TTT.Server.GameLogic;

public class UsersManager
{
    private readonly IUserRepository userRepository;
    private readonly Dictionary<int, ServerConnection> connetions;

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

    public void Disconnect(int peerId, NetworkServer server)
    {
        var connection = GetConnection(peerId);
        if (connection.User != null)
        {
            var userId = connection.User.Id;
            userRepository.SetOffline(userId);
            server.NotifyOtherPlayers(
                userRepository.GetTotalCount(),
                GetTopPlayers(),
                GetOtherConnectionIds(peerId));
        }

        connetions.Remove(peerId);
    }

    public ServerConnection GetConnection(int peerId) => connetions[peerId];

    public ServerConnection GetConnection(string userId)
    {
        return connetions.FirstOrDefault(x => x.Value.User.Id == userId).Value;
    }

    public int[] GetOtherConnectionIds(int excludeConnectionId)
    {
        return connetions.Keys
                         .Where(x => x != excludeConnectionId)
                         .ToArray();
    }

    public PlayerNetDto[] GetTopPlayers()
    {
        return userRepository.GetQuery().OrderByDescending(x => x.Score)
                             .Take(10)
                             .Select(x => new PlayerNetDto
                              {
                                  Username = x.Id,
                                  Score = x.Score,
                                  IsOnline = x.IsOnline,
                              })
                             .ToArray();
    }
}