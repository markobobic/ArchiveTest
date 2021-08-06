using ArchiveData.Extensions;
using ArchiveData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace ArchiveData.DB
{
    public class ApplicationDBContext : DbContext
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
        }


        public override int SaveChanges()
        {
           
            if (ChangeTracker.Entries().AsParallel()
                .Count(x => x.State == EntityState.Added &&
                            typeof(InputNotificationEventEntity).IsAssignableFrom(x.Entity.GetType())) > 0)
            {
                ArchiveTable();
                return 0;
            }
            return base.SaveChanges();

        }
        private void ArchiveTable()
        {
            int maxLimit = int.MaxValue;
            int archiveLimit = 0;
            var entitiesListCount = InputNotificationEventEntities.Count();
            DetermineMaxLimit(ref maxLimit, entitiesListCount);
            DetermineArchivingLimit(ref archiveLimit, entitiesListCount); 
            if (entitiesListCount > maxLimit)
            {
                var archivedInputs = InputNotificationEventEntities.OrderByDescending(x => x.SourceEventTimeStampUtc).Take(archiveLimit);
                ArchivedInputNotifications.BulkInsert(archivedInputs.ToArchived());
                InputNotificationEventEntities.BulkDelete(archivedInputs);

            }
            
        }

        private void DetermineMaxLimit(ref int maxLimit,int entitiesListCount)
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
                default:
                    break;
            }
        }
        public static void DetermineArchivingLimit(ref int archiveLimit, int entitiesListCount)
        {
            switch (entitiesListCount)
            {
                case 10010501:
                    archiveLimit = 2000000;
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
                    break;
            }
        }
        
    }
}
