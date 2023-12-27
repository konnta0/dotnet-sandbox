using System.Reflection;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class CoreDbContext : DbContext
{
    public DbSet<ExampleEntity> ExampleEntities { get; init; } = default!;

    public CoreDbContext(DbContextOptions<CoreDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            Console.WriteLine("EntityType: " + entityType.DisplayName());
            var properties = entityType.GetProperties().ToArray();
            foreach (var property in properties)
            {
                Console.WriteLine("Property: " + property.Name);
                var attr = property.ClrType.GetCustomAttribute<ColumnOrderAttribute>();
                var pattr = property.PropertyInfo?.GetCustomAttribute<ColumnOrderAttribute>();
                if (pattr is not null) attr = pattr;
                if (attr is null) continue;
                Console.WriteLine("ColumnOrder: " + attr.Order);
                property.SetAnnotation(CustomAnnotationNames.ColumnOrder, attr.Order);
                property.SetColumnOrder(attr.Order);
            }
        }
    }
}