using ArchiveData.Configuration;
using ArchiveData.DB;
using ArchiveData.Extensions;
using ArchiveData.Model;
using ArchiveData.Tests;
using ArchiveData.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveData.Services
{
    public class TestArchiveService
    {
        private readonly MockDataService _mockDataService;
        private readonly DBConfigEnum _dbConfig;
        private readonly SqlServerDBContext _db;
        public TestArchiveService(MockDataService mockDataService, DBConfigEnum dbConfig)
        {
            _mockDataService = mockDataService;
            _dbConfig = dbConfig;
            _db = dbConfig switch
            {
                DBConfigEnum.MySql => new MySqlDBContext(),
                DBConfigEnum.SqlServer => new SqlServerDBContext(),
                DBConfigEnum.PostgreSql => new PostgreSqlDBContext(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbConfig), $"Not expected config value: {dbConfig}"),
            };
        }

        public async Task RunOnlyBulkTests()
        {

            var configBulkInsertTest6 = new TestConfig(1000000,
                                                       TestEnum.Test6,
                                                       TestType.OnlyBulkInsert);
            var result = TestBulkInsertOnly(configBulkInsertTest6, _db);
            TestUI.ShowMeasurementsResults(await result);

            //    var configBulkInsertTest4 = new TestConfig(5000000,
            //                                              TestEnum.Test4,
            //                                              TestType.OnlyBulkInsert);
            //    var result1 = TestBulkInsertOnly(configBulkInsertTest4, _db);
            //    TestUI.ShowMeasurementsResults(await result1);

            //    var configBulkInsertTest1 = new TestConfig(10000000,
            //                                             TestEnum.Test1,
            //                                             TestType.OnlyBulkInsert);
            //    var result2 = TestBulkInsertOnly(configBulkInsertTest1, _db);
            //    TestUI.ShowMeasurementsResults(await result2);
        }
        public async Task RunOnlySaveChangesTests()
        {

            var configBulkInsertTest5 = new TestConfig(600,
                                                      0,
                                                      0,
                                                     TestEnum.Test5,
                                                     TestType.OnlySaveChanges);
            var result5 = TestSaveChangesOnly(configBulkInsertTest5, _db);
            TestUI.ShowMeasurementsResults(await result5);

            //var configBulkInsertTest6 = new TestConfig(1000000,
            //                                         10500,
            //                                         200000,
            //                                        TestEnum.Test6,
            //                                        TestType.OnlySaveChanges);
            //var result6 = TestSaveChangesOnly(configBulkInsertTest6, _db);
            //TestUI.ShowMeasurementsResults(await result6);

            //var configBulkInsertTest1 = new TestConfig(10000000,
            //                                           10501,
            //                                           2000000,
            //                                          TestEnum.Test1,
            //                                          TestType.OnlySaveChanges);
            //var result1 = TestSaveChangesOnly(configBulkInsertTest1, _db);
            //TestUI.ShowMeasurementsResults(await result1);

            //var configBulkInsertTest2 = new TestConfig(10000000,
            //                                            10500,
            //                                            200000,
            //                                           TestEnum.Test2,
            //                                           TestType.OnlySaveChanges);
            //var result2 = TestSaveChangesOnly(configBulkInsertTest2, _db);
            //TestUI.ShowMeasurementsResults(await result2);

            //var configBulkInsertTest3 = new TestConfig(5000000,
            //                                            10501,
            //                                            1000000,
            //                                           TestEnum.Test3,
            //                                           TestType.OnlySaveChanges);
            //var result3 = TestSaveChangesOnly(configBulkInsertTest3, _db);
            //TestUI.ShowMeasurementsResults(await result3);

            //var configBulkInsertTest4 = new TestConfig(5000000,
            //                                           10500,
            //                                           200000,
            //                                          TestEnum.Test4,
            //                                          TestType.OnlySaveChanges);
            //var result4 = TestSaveChangesOnly(configBulkInsertTest4, _db);
            //TestUI.ShowMeasurementsResults(await result4);

        }


        private async Task<TestResult> TestBulkInsertOnly(TestConfig config, SqlServerDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var initialData = _mockDataService.Populate(config, InsertionTypeEnum.Initial);
            if (db.InputNotificationEventDefinitionEntities.Count() == 0)
            {
                db.InputNotificationEventDefinitionEntities.Add(initialData.FirstOrDefault().EventDefinition);
                db.SaveChanges();
            }
            TestUI.ShowStartOfTest(config);
            for (int i = 1; i <= 3; i++)
            {
                stopWatch.Start();
                await db.InputNotificationEventEntities.AddRangeAsync(initialData);
                initialData.ForEach(x => db.Entry(x.EventDefinition).State = EntityState.Modified);
                initialData.ForEach(x => db.Entry(x).State = EntityState.Added);
                await db.SaveChangesAsync();
                stopWatch.Stop();
                Console.WriteLine("Initial date populated");
                TimeSpan ts = stopWatch.Elapsed;
                TestUI.WriteOrdinalIterations(i, ts);
                timeSpans.Add(ts);
                stopWatch.Reset();
                Reset(_dbConfig);
            }

            return new TestResult(timeSpans.Average(x => x.TotalSeconds), config.TestEnum, config.TestType);

        }

        private async Task<TestResult> TestSaveChangesOnly(TestConfig config, SqlServerDBContext db)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Stopwatch stopWatch = new Stopwatch();
            var initialData = _mockDataService.Populate(config, InsertionTypeEnum.Initial);
            var additionalData = _mockDataService.Populate(config, InsertionTypeEnum.Additional);
            if (db.InputNotificationEventDefinitionEntities.Count() == 0)
            {
                db.InputNotificationEventDefinitionEntities.Add(initialData.FirstOrDefault().EventDefinition);
                await db.SaveChangesAsync();
            }
           
            TestUI.ShowStartOfTest(config);
            for (int i = 1; i <= 3; i++)
            {
                var list2 = _db.ChangeTracker.Entries().ToList();

                initialData.ForEach(x => _db.Entry(x).State = EntityState.Added);
                    initialData.ForEach(x => _db.Entry(x.EventDefinition).State = EntityState.Detached);
               var list1 = _db.ChangeTracker.Entries().ToList();

                 db.InputNotificationEventEntities.AddRange(initialData);                    
                
                Console.WriteLine("Initial date populated and additional");
                stopWatch.Start();
                var list = _db.ChangeTracker.Entries().ToList();
                db.SaveChanges();
                ArchiveTable(int.MaxValue);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                TestUI.WriteOrdinalIterations(i, ts);
                timeSpans.Add(ts);
                stopWatch.Reset();
                Reset(_dbConfig);
            }
            return new TestResult(timeSpans.Average(x => x.TotalSeconds), config.TestEnum, config.TestType);

        }

        public void Reset(DBConfigEnum dBConfig)
        {
            if (dBConfig == DBConfigEnum.PostgreSql)
            {
                _db.Database.ExecuteSqlRaw(@"TRUNCATE TABLE ""InputNotificationEventEntities""");
                _db.Database.ExecuteSqlRaw(@"TRUNCATE TABLE ""ArchivedInputNotifications""");
                return;
            }
            _db.Database.ExecuteSqlRaw("TRUNCATE TABLE InputNotificationEventEntities");
            _db.Database.ExecuteSqlRaw("TRUNCATE TABLE ArchivedInputNotifications");

        }
        private void ArchiveTable(int entitiesListCount)
        {
            int maxLimit = int.MaxValue-5;
            int archiveLimit = 250;
            if (entitiesListCount > maxLimit)
            {
                var list2 = _db.ChangeTracker.Entries().ToList();

                var archivedInputs = _db.InputNotificationEventEntities.OrderByDescending(x => x.SourceEventTimeStampUtc)
                    .Take(archiveLimit);
                var archived = archivedInputs.ToArchived().ToList();
                archived.ForEach(x => _db.Entry(x.EventDefinition).State = EntityState.Detached);
                archived.ForEach(x => _db.Entry(x).State = EntityState.Added);
                _db.ArchivedInputNotifications.AddRangeAsync(archived);
                _db.InputNotificationEventEntities.RemoveRange(archivedInputs);
                //_db.ChangeTracker.Entries<InputNotificationEventDefinitionEntity>().ToList().ForEach(p => p.State = EntityState.Unchanged);
                //_db.ChangeTracker.Entries<ArchivedInputNotification>().ToList().ForEach(p => p.State = EntityState.Added);
                var list = _db.ChangeTracker.Entries().ToList();
                _db.SaveChanges();
            }

        }
    }
}
