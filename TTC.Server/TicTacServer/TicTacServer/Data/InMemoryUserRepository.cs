namespace TicTacServer.Data;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> users;

    public InMemoryUserRepository()
    {
        users = new List<User>
        {
            new()
            {
                Id = "User1",
                Password = "Pass1",
                IsOnline = true,
                Score = 10,
            },
            new()
            {
                Id = "User2",
                Password = "Pass2",
                IsOnline = true,
                Score = 35,
            },
        };
    }

    public void Update(User entity)
    {
        var index = users.FindIndex(x => x.Id == entity.Id);
        users[index] = entity;
    }

    public void Add(User entity) => users.Add(entity);

    public User Get(string id) => users.FirstOrDefault(x => x.Id == id);

    public IQueryable<User> GetQuery() => users.AsQueryable();

    public ushort GetTotalCount() => (ushort) users.Count(x => x.IsOnline);

    public void Delete(string id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        users.Remove(user);
    }

    public void SetOnline(string id) => Get(id).IsOnline = true;

    public void SetOffline(string id) => Get(id).IsOnline = false;
}