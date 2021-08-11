using ArchiveData.Extensions;
using ArchiveData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ArchiveData.DB
{
    public class SqlServerDBContext : DbContext
    {
        public DbSet<InputNotificationEventEntity> InputNotificationEventEntities { get; set; }
        public DbSet<ArchivedInputNotification> ArchivedInputNotifications { get; set; }

        public DbSet<InputNotificationEventDefinitionEntity> InputNotificationEventDefinitionEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=ArchiveDataDb;Trusted_Connection=True");
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

            modelBuilder.Entity<InputNotificationEventDefinitionEntity>()
           .Property(a => a.Id).HasColumnType("char(36)");

            //Input notification table
            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("smalldatetime");
            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.SourceEventTimeStampUtc).HasColumnType("smalldatetime");

            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.ClientId).HasColumnType("char(36)");

            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.EventDefinitionId).HasColumnType("char(36)");
            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.EventId).HasColumnType("char(36)");
            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.EventTargetId).HasColumnType("char(36)");

            //archive table
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("smalldatetime");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.SourceEventTimeStampUtc).HasColumnType("smalldatetime");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.ClientId).HasColumnType("char(36)");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventDefinitionId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventTargetId).HasColumnType("char(36)");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (ChangeTracker.Entries().AsParallel()
                .Count(x => x.State == EntityState.Added &&
                            typeof(InputNotificationEventEntity).IsAssignableFrom(x.Entity.GetType())) > 0)
            {
                await ArchiveTable();
                return await Task.FromResult(0);
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
       
        protected async Task ArchiveTable()
        {
            int maxLimit = int.MaxValue;
            int archiveLimit = 0;
            var entitiesListCount = await InputNotificationEventEntities.CountAsync();
            DetermineMaxLimit(ref maxLimit, entitiesListCount);
            DetermineArchivingLimit(ref archiveLimit, entitiesListCount); 
            if (entitiesListCount > maxLimit)
            {
                var archivedInputs = InputNotificationEventEntities.OrderByDescending(x => x.SourceEventTimeStampUtc)
                    .Take(archiveLimit);
                ArchivedInputNotifications.BulkInsert(archivedInputs.ToArchived());
                await InputNotificationEventEntities.BulkDeleteAsync(archivedInputs);

            }
            
        }

        protected void DetermineMaxLimit(ref int maxLimit,int entitiesListCount)
        {
            switch (entitiesListCount)
            {
                case 10010500 or 10010501:
                    maxLimit = 10010000;
                    break;
                case 5010500 or 5010501:
                    maxLimit = 5010000;
                    break;
                case 1010500 or 1010501:
                    maxLimit = 1010000;
                    break;
                default: maxLimit = 500;
                    break;
            }
        }
        protected  void DetermineArchivingLimit(ref int archiveLimit, int entitiesListCount)
        {
            switch (entitiesListCount)
            {
                case 10010501:
                    archiveLimit = 2000000;
                    break;
                case 10010500:
                    archiveLimit = 200000;
                    break;
                case 1001050:
                    archiveLimit = 200000;
                    break;
                case 5010501:
                    archiveLimit = 1000000;
                    break;
                case 5010500:
                    archiveLimit = 200000;
                    break;
                case 1010501:
                    archiveLimit = 500000;
                    break;
                case 1010500:
                    archiveLimit = 200000;
                    break;
                default:
                    archiveLimit = 250;
                    break;
            }
        }
        
    }
}
