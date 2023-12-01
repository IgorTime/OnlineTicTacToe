namespace TTT.Server.Data;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> users = new();

    public void Update(User entity)
    {
        users[entity.Id] = entity;
    }

    public void Add(User entity)
    {
        users[entity.Id] = entity;
    }

    public User Get(string id) => users[id];

    public IQueryable<User> GetQuery() => users.Values.AsQueryable();

    public ushort GetTotalCount() => (ushort) users.Values.Count(x => x.IsOnline);

    public void Delete(string id)
    {
        users.Remove(id);
    }

    public void SetOnline(string id) => Get(id).IsOnline = true;

    public void SetOffline(string id) => Get(id).IsOnline = false;
}