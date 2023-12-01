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

    public bool TryGet(string id, out User entity) => users.TryGetValue(id, out entity);

    public IQueryable<User> GetQuery() => users.Values.AsQueryable();

    public ushort GetTotalCount() => (ushort) users.Values.Count(x => x.IsOnline);

    public void Delete(string id)
    {
        users.Remove(id);
    }

    public void SetOnline(string id)
    {
        if (TryGet(id, out var user))
        {
            user.IsOnline = true;
        }
    }

    public void SetOffline(string id)
    {
        if (TryGet(id, out var user))
        {
            user.IsOnline = false;
        }
    }
}