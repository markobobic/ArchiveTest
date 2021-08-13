using ArchiveData.Extensions;
using ArchiveData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArchiveData.DB
{
    public class PostgreSqlDBContext : SqlServerDBContext
    {
        private string _connectionString = @"Host=localhost;Database=ReconciliationPostgreSQL;Username=postgres;Password=root";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
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
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("TIMESTAMP(0)");
            modelBuilder.Entity<InputNotificationEventEntity>()
          .Property(a => a.SourceEventTimeStampUtc).HasColumnType("TIMESTAMP(0)");

            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.ClientId).HasColumnType("varchar(36)");

            modelBuilder.Entity<InputNotificationEventEntity>()
          .Property(a => a.EventDefinitionId).HasColumnType("char(36)");
            modelBuilder.Entity<InputNotificationEventEntity>()
          .Property(a => a.EventId).HasColumnType("char(36)");
            modelBuilder.Entity<InputNotificationEventEntity>()
          .Property(a => a.EventTargetId).HasColumnType("char(36)");

            //archive table
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("TIMESTAMP(0)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.SourceEventTimeStampUtc).HasColumnType("TIMESTAMP(0)");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.ClientId).HasColumnType("varchar(36)");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventDefinitionId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventTargetId).HasColumnType("char(36)");

        }


        public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (CheckForNotificationAdded())
            {
                var entitiesListCount = ChangeTracker.Entries().Count();
                ClearNotificationStateAdded();
                ArchiveTable(entitiesListCount);
                return base.SaveChangesAsync(cancellationToken);

            }
            return base.SaveChangesAsync(cancellationToken);

        }
        private void ArchiveTable(int entitiesListCount)
        {
            int maxLimit = int.MaxValue;
            int archiveLimit = 0;
            DetermineMaxLimit(ref maxLimit, entitiesListCount);
            DetermineArchivingLimit(ref archiveLimit, entitiesListCount);
            if (entitiesListCount > maxLimit)
            {
                var archivedInputs = InputNotificationEventEntities.OrderByDescending(x => x.SourceEventTimeStampUtc)
                    .Take(archiveLimit);
                var archived = archivedInputs.ToArchived().ToList();
                ArchivedInputNotifications.AddRangeAsync(archived);
                InputNotificationEventEntities.RemoveRange(archivedInputs);

                archived.ForEach(x => Entry(x.EventDefinition).State = EntityState.Detached);
                archived.ForEach(x => Entry(x).State = EntityState.Added);
            }

        }
    }

}
