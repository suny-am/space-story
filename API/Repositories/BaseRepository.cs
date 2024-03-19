using Microsoft.AspNetCore.Mvc;

namespace API.Repositories;

public class BaseRepository<T>(GameDbContext context) : IRepository<T> where T : class
{

    private readonly GameDbContext gameDbContext = context;

    public async Task<ActionResult<T>> Add(T entity)
    {
        return await Task.Run(() =>
        {
            return gameDbContext.Add(entity).Entity;
        });
    }

    public async Task<ActionResult<IEnumerable<T>>> All()
    {
        return await Task.Run(() =>
        {
            return gameDbContext.Set<T>().ToList();
        });
    }

    public async virtual Task<ActionResult<IEnumerable<T?>>> Find(Func<T, bool> predicate)
    {
        return await Task.Run(() =>
        {
            return gameDbContext.Set<T>().Where(predicate).ToList();
        });
    }

    public async Task<ActionResult<T?>> GetByID(Guid id)
    {
        return await Task.Run(() =>
        {
            return gameDbContext.Set<T>().Find(id);
        }
        );
    }

    public async Task<ActionResult<T?>> Remove(Guid id)
    {
        var target = await GetByID(id) ?? throw new KeyNotFoundException($"[Entity ID {id}] does not exist");

        return gameDbContext.Remove(target).Entity;
    }

    public async virtual Task SaveChanges()
    {
        await gameDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<T?>> Update(T entity)
    {
        return await Task.Run(() =>
        {
            return gameDbContext.Update(entity).Entity;
        });
    }

    public async Task<bool> Any(Func<T, bool> predicate)
    {
        return await Task.Run(() =>
        {
            var exists = gameDbContext.Set<T>().First(predicate);
            return exists is null;
        });
    }
}

