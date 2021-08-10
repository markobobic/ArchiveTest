using ArchiveData.Configuration;
using ArchiveData.Services;
using System.Threading.Tasks;
using static System.Console;

namespace ArchiveData
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //SQL SERVER
            WriteLine("********************************SQLSERVER STARTING********************************");
            TestArchiveService servicesSqlServer = new TestArchiveService(new MockDataService(), DBConfigEnum.SqlServer);
            await servicesSqlServer.RunOnlySaveChangesTests();
            await servicesSqlServer.RunOnlyBulkTests();
            WriteLine("**********************SQL SERVER FINISHED************************************");

            WriteLine("********************************MYSQL STARTING********************************");
            //MY SQL - time stamp => long
            TestArchiveService servicesMySql = new TestArchiveService(new MockDataService(), DBConfigEnum.SqlServer);
            await servicesMySql.RunOnlySaveChangesTests();
            await servicesMySql.RunOnlyBulkTests();
            //servicesMySql.Reset();

            //PostgresSQL
            TestArchiveService servicesPostgreSql = new TestArchiveService(new MockDataService(), DBConfigEnum.PostgreSql);
            await servicesMySql.RunOnlySaveChangesTests();
            await servicesMySql.RunOnlyBulkTests();
            ReadLine();

        }
        
        
    }
}
