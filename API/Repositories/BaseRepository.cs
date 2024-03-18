namespace API.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{

    private readonly GameDbContext gameDbContext = default!;

    public T Add()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> All()
    {
        return gameDbContext.Set<T>().ToList();
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return gameDbContext.Set<T>().Where(predicate);
    }

    public T GetByID(Guid id)
    {
        throw new NotImplementedException();
    }

    public T Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public T Update(Guid id)
    {
        throw new NotImplementedException();
    }
}

