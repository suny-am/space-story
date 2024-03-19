using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Repositories;

public class GameDbContext() : DbContext()
{

    public DbSet<SaveState> SaveStates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}