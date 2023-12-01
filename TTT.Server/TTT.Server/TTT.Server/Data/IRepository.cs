namespace TTT.Server.Data;

public interface IRepository<T> where T : class
{
    void Update(T entity);
    void Add(T entity);
    bool TryGet(string id, out T entity);
    IQueryable<T> GetQuery();
    ushort GetTotalCount();
    void Delete(string id);
}