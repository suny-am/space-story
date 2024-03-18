using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Repositories;

public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
{

    public DbSet<SaveState> SaveStates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}