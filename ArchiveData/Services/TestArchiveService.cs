using ArchiveData.DB;
using ArchiveData.Extensions;
using ArchiveData.Tests;
using ArchiveData.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArchiveData.Services
{
    public static class TestArchiveService
    {
        public static TestResult TestBulkInsertOnly(TestConfig config,ApplicationDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var mockData = MockDataService.Populate(config);
            mockData.Select(l => db.Entry(l).State = EntityState.Added).FirstOrDefault();
            TestUI.ShowStartOfTest(config);
            for (int i = 1; i <= 3; i++)
            {
                stopWatch.Start();
                db.InputNotificationEventEntities.BulkInsert(mockData);
                stopWatch.Stop();
             
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"{i.ToOrdinalNumbers()} iteration: {ts}");
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
