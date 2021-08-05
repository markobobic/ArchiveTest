using ArchiveData.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArchiveData.Tests
{
    public static class TestArchive
    {
        public static TestResult TestBulkInsertOnly(TestConfig config,ApplicationDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var mockData = MockData.Populate(config);
            mockData.Select(l => db.Entry(l).State = EntityState.Added).FirstOrDefault();
            for (int i = 0; i < 3; i++)
            {
                stopWatch.Start();
                db.InputNotificationEventEntities.BulkInsert(mockData);
                stopWatch.Stop();
             
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine(ts);
                timeSpans.Add(ts);
                stopWatch.Reset();
                Reset(db);
            }
            
            return new TestResult(timeSpans.Average(x => x.TotalSeconds),config.TestEnum,config.TestType);
           
        }

        public static void Reset(ApplicationDBContext db)
        {
            
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE InputNotificationEventEntities");
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE InputNotificationEventEntities");
            db.SaveChanges();
        }

        
    }
}
