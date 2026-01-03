using System;
using Fintech_App.Model.Domain;
using Fintech_App.Model.Others;
using Microsoft.EntityFrameworkCore;

namespace Fintech_App.Model.Db
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                b.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                b.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                b.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            // Grab all entities that are being Added or Modified and implement IAuditable
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseSchema &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseSchema)entry.Entity;
                var now = DateTime.UtcNow; // Always use UTC for databases

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }
        }
    }
}
