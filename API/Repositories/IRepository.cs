using Microsoft.AspNetCore.Mvc;

namespace API.Repositories;

public interface IRepository<T> where T : class
{
    public Task<ActionResult<IEnumerable<T>>> All();
    public Task<ActionResult<T?>> GetByID(Guid id);
    public Task<ActionResult<IEnumerable<T?>>> Find(Func<T, bool> predicate);
    public Task<ActionResult<T>> Add(T entity);
    public Task<ActionResult<T?>> Remove(Guid id);
    public Task<ActionResult<T?>> Update(T entity);
    public Task<bool> Any(Func<T, bool> predicate);
    public Task SaveChanges();
}