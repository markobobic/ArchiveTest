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
            var entitiesListCount = InputNotificationEventEntities.Count();
            DetermineMaxLimit(ref maxLimit, entitiesListCount);
            if (entitiesListCount > maxLimit)
            {
                var archivedInputs = InputNotificationEventEntities.OrderByDescending(x => x.SourceEventTimeStampUtc).Take(6000);
                this.BulkInsert(archivedInputs.ToArchived().ToList());
                this.BulkDelete(archivedInputs.ToList());

            }
            
        }

        private void DetermineMaxLimit(ref int maxLimit,int entitiesListCount)
        {
            switch (entitiesListCount)
            {
                case 10010500:
                    maxLimit = 10010000;
                    break;
                case 5010000:
                    maxLimit = 5010500;
                    break;
                case 1010000:
                    maxLimit = 1010500;
                    break;
                default:
                    break;
            }
        }

        //to delete all
        //public override int SaveChanges()
        //{

        //    var entitiesList = InputNotificationEventEntities.ToArray();
        //    var archivedList = ArchivedInputNotifications.ToArray();


        //    ArchivedInputNotifications.BulkDelete(archivedList);
        //    InputNotificationEventEntities.BulkDelete(entitiesList);



        //    return base.SaveChanges();
        //}
    }
}
