using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Repositories;

public class GameDbContext : DbContext
{
    private readonly IConfiguration? configuration;

    public DbSet<SaveState> SaveStates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(configuration?.GetConnectionString("SQLite"));
    }
}