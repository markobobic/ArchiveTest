using ArchiveData.DB;
using ArchiveData.Services;
using ArchiveData.Tests;
using ArchiveData.UI;
using static System.Console;

namespace ArchiveData
{
    class Program
    {
        static void Main(string[] args)
        {
            //TEST 1
            //10 000 000 bulk insert + 10 500 
            // limit 10 010 000
            //arhivaranje 2 000 000 

            //TEST 2
            //10 000 000 bulk insert + 10 500 
            // limit 10 010 000
            //arhivaranje 200 000 

            //TEST 3
            //5 000 000 bulk insert + 10 500 
            // limit 5 010 000
            //arhivaranje 1 000 000 

            //TEST 4
            //5 000 000 bulk insert + 10 500 
            // limit 5 010 000
            //arhivaranje 200 000

            //TEST 5
            //1 000 000 bulk insert + 10 500 
            // limit 1 010 000
            //arhivaranje 500 000

            //TEST 6
            //1 000 000 bulk insert + 10 500 
            // limit 1 010 000
            //arhivaranje 200 000

            // testirati prvobitni bulk i SaveChanges()
            // za svaki test 3 puta pokrenuti i uzeti srednju vrednost 
            // MySQL i MSSqlServera poredjenje preformansi 

            using var db = new ApplicationDBContext();

            var configBulkInsertTest6 = new TestConfig(1000000,
                                                       TestEnum.Test6,
                                                       TestType.OnlyBulkInsert);
            var result = TestArchiveService.TestBulkInsertOnly(configBulkInsertTest6, db);
            TestUI.ShowMeasurementsResults(result);

            var configBulkInsertTest5 = new TestConfig(5000000,
                                                      TestEnum.Test5,
                                                      TestType.OnlyBulkInsert);
            var result1 = TestArchiveService.TestBulkInsertOnly(configBulkInsertTest5, db);
            TestUI.ShowMeasurementsResults(result1);

            var configBulkInsertTest1 = new TestConfig(10000000,
                                                     TestEnum.Test1,
                                                     TestType.OnlyBulkInsert);
            var result2 = TestArchiveService.TestBulkInsertOnly(configBulkInsertTest1, db);
            TestUI.ShowMeasurementsResults(result2);
            //TestArchive.Reset(db);
            ReadLine();

        }
        
    }
}
