using ArchiveData.Extensions;
using ArchiveData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArchiveData.DB
{
    public class MySqlDBContext : SqlServerDBContext
    {
        private string _connectionString = @"server=127.0.0.1; port=3306; database=ReconciliationMySQL; user=root; password=root; Persist Security Info=false; Connect Timeout=300";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<InputNotificationEventDefinitionEntity>()
              .Property(e => e.EventType)
              .HasConversion(new EnumToStringConverter<EventType>());

            modelBuilder
              .Entity<InputNotificationEventDefinitionEntity>()
              .Property(e => e.TermType)
              .HasConversion(new EnumToStringConverter<TermType>());
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ChangeTracker.Entries().AsParallel()
                .Count(x => x.State == EntityState.Added &&
                            typeof(InputNotificationEventEntity).IsAssignableFrom(x.Entity.GetType())) > 0)
            {
                await ArchiveTable();
                return 0;
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

    }
}
