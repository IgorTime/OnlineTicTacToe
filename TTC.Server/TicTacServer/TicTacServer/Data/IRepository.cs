namespace TicTacServer.Data;

public interface IRepository<T> where T : class
{
    void Update(T entity);
    void Add(T entity);
    T Get(string id);
    IQueryable<T> GetQuery();
    ushort GetTotalCount();
    void Delete(string id);
}