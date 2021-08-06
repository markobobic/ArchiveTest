using ArchiveData.Configuration;
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
    public  class TestArchiveService
    {
        private readonly MockDataService _mockDataService;
        public TestArchiveService(MockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        public void RunOnlyBulkTests(ApplicationDBContext db)
        {
            var configBulkInsertTest6 = new TestConfig(1000000,
                                                       TestEnum.Test6,
                                                       TestType.OnlyBulkInsert);
            var result = TestBulkInsertOnly(configBulkInsertTest6, db);
            TestUI.ShowMeasurementsResults(result);

            var configBulkInsertTest4 = new TestConfig(5000000,
                                                      TestEnum.Test4,
                                                      TestType.OnlyBulkInsert);
            var result1 = TestBulkInsertOnly(configBulkInsertTest4, db);
            TestUI.ShowMeasurementsResults(result1);

            var configBulkInsertTest1 = new TestConfig(10000000,
                                                     TestEnum.Test1,
                                                     TestType.OnlyBulkInsert);
            var result2 = TestBulkInsertOnly(configBulkInsertTest1, db);
            TestUI.ShowMeasurementsResults(result2);
        }
        public void RunOnlySaveChangesTests(ApplicationDBContext db)
        {
            var configBulkInsertTest1 = new TestConfig(10000000,
                                                        10501,
                                                        2000000,
                                                       TestEnum.Test1,
                                                       TestType.OnlySaveChanges);
            var result1 = TestSaveChangesOnly(configBulkInsertTest1, db);
            TestUI.ShowMeasurementsResults(result1);

            var configBulkInsertTest2 = new TestConfig(10000000,
                                                        10500,
                                                        200000,
                                                       TestEnum.Test2,
                                                       TestType.OnlySaveChanges);
            var result2 = TestSaveChangesOnly(configBulkInsertTest2, db);
            TestUI.ShowMeasurementsResults(result2);

            var configBulkInsertTest3 = new TestConfig(5000000,
                                                        10501,
                                                        1000000,
                                                       TestEnum.Test3,
                                                       TestType.OnlySaveChanges);
            var result3 = TestSaveChangesOnly(configBulkInsertTest3, db);
            TestUI.ShowMeasurementsResults(result3);

            var configBulkInsertTest4 = new TestConfig(5000000,
                                                       10500,
                                                       200000,
                                                      TestEnum.Test4,
                                                      TestType.OnlySaveChanges);
            var result4 = TestSaveChangesOnly(configBulkInsertTest4, db);
            TestUI.ShowMeasurementsResults(result4);

            var configBulkInsertTest5 = new TestConfig(1000000,
                                                      10501,
                                                      500000,
                                                     TestEnum.Test5,
                                                     TestType.OnlySaveChanges);
            var result5 = TestSaveChangesOnly(configBulkInsertTest5, db);
            TestUI.ShowMeasurementsResults(result5);

            var configBulkInsertTest6 = new TestConfig(1000000,
                                                     10500,
                                                     200000,
                                                    TestEnum.Test6,
                                                    TestType.OnlySaveChanges);
            var result6 = TestSaveChangesOnly(configBulkInsertTest6, db);
            TestUI.ShowMeasurementsResults(result6);

        }


        private  TestResult TestBulkInsertOnly(TestConfig config,ApplicationDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var initialData = _mockDataService.Populate(config, InsertionTypeEnum.Initial);
            initialData.Select(l => db.Entry(l).State = EntityState.Added).FirstOrDefault();
            TestUI.ShowStartOfTest(config);
            for (int i = 1; i <= 3; i++)
            {
                stopWatch.Start();
                db.InputNotificationEventEntities.BulkInsert(initialData);
                stopWatch.Stop();
             
                TimeSpan ts = stopWatch.Elapsed;
                TestUI.WriteOrdinalIterations(i, ts);
                timeSpans.Add(ts);
                stopWatch.Reset();
                Reset(db);
            }
            
            return new TestResult(timeSpans.Average(x => x.TotalSeconds),config.TestEnum,config.TestType);
           
        }

        private TestResult TestSaveChangesOnly(TestConfig config, ApplicationDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var initialData = _mockDataService.Populate(config, InsertionTypeEnum.Initial);
            var additionalData = _mockDataService.Populate(config, InsertionTypeEnum.Additional);
            additionalData.Select(l => db.Entry(l).State = EntityState.Added).FirstOrDefault();
            TestUI.ShowStartOfTest(config);
            for (int i = 1; i <= 3; i++)
            {
                db.InputNotificationEventEntities.BulkInsert(initialData);
                db.InputNotificationEventEntities.BulkInsert(additionalData);
                stopWatch.Start();
                db.SaveChanges();
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                TestUI.WriteOrdinalIterations(i, ts);
                timeSpans.Add(ts);
                stopWatch.Reset();
                Reset(db);
            }
            return new TestResult(timeSpans.Average(x => x.TotalSeconds), config.TestEnum, config.TestType);

        }

        public void Reset(ApplicationDBContext db)
        {
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE InputNotificationEventEntities");
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE ArchivedInputNotifications");
           
        }

        
    }
}
