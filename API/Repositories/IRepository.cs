namespace API.Repositories;

public interface IRepository<T> where T : class
{
    public IEnumerable<T> All();
    public T GetByID(Guid id);
    public IEnumerable<T> Find(Func<T, bool> predicate);
    public T Add();
    public T Remove(Guid id);
    public T Update(Guid id);
    public void SaveChanges();
}