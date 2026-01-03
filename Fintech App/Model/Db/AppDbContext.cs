using System;
using Fintech_App.Model.Domain;
using Fintech_App.Model.Others;
using Microsoft.EntityFrameworkCore;

namespace Fintech_App.Model.Db
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts => Set<Account>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(b =>
            {
                b.HasKey(o => o.Id); //Primary Key

                //FK Relationship
                b.HasOne(o => o.User)
                .WithMany(c => c.Accounts)
                .HasForeignKey(o => o.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.AccountNumber)
                                    .IsUnique();

                b.Property(o => o.AccountNumber)
                .IsRequired();

                b.Property(o => o.Balance)
                .HasPrecision(18, 2);

                b.Property(o => o.Currency)
                .HasConversion<string>()
                .HasMaxLength(20);

                b.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });


            modelBuilder.Entity<User>(b =>
                        {

                            b.HasIndex(x => x.Email)
                                                .IsUnique();

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


            OnConfiguringDecimals(modelBuilder);
        }

        private static void OnConfiguringDecimals(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model
                  .GetEntityTypes()
                   .SelectMany(t => t.GetProperties())
                   .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
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
