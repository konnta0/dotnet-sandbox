using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class CoreDbContext : DbContext
{
    public DbSet<ExampleEntity> ExampleEntities { get; init; } = default!;

    public CoreDbContext(DbContextOptions<CoreDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
}