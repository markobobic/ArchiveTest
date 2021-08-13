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

            modelBuilder.Entity<InputNotificationEventDefinitionEntity>()
           .Property(a => a.Id).HasColumnType("char(36)");

            //Input notification table
            modelBuilder.Entity<InputNotificationEventEntity>()
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("TIMESTAMP");
            modelBuilder.Entity<InputNotificationEventEntity>()
          .Property(a => a.SourceEventTimeStampUtc).HasColumnType("TIMESTAMP");

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
           .Property(a => a.AcknowledgmentTimeStampUtc).HasColumnType("TIMESTAMP");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.SourceEventTimeStampUtc).HasColumnType("TIMESTAMP");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.ClientId).HasColumnType("varchar(36)");

            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventDefinitionId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventId).HasColumnType("char(36)");
            modelBuilder.Entity<ArchivedInputNotification>()
           .Property(a => a.EventTargetId).HasColumnType("char(36)");

        }


        //public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    if (CheckForNotificationAdded())
        //    {
        //        var entitiesListCount = ChangeTracker.Entries().Count();
        //        ArchiveTable(entitiesListCount);
        //        var tracker = ChangeTracker.Entries().ToList();
        //        return base.SaveChangesAsync();

        //    }
        //    return base.SaveChangesAsync();
        //}
       
    }
}
