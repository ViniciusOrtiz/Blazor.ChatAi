using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<DocumentEntity> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("App");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeEntries<int>();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void ChangeEntries<TKey>()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity<TKey>>()
                .Where(q => q.State is EntityState.Added or EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }

                var dateProperties = entry.Entity.GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

                foreach (var property in dateProperties)
                {
                    var currentValue = (DateTime?)property.GetValue(entry.Entity);

                    if (currentValue is { Kind: DateTimeKind.Local })
                    {
                        property.SetValue(entry.Entity, currentValue.Value.ToUniversalTime());
                    }
                }
            }
        }
    }
}
