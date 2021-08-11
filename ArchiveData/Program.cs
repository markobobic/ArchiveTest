using ArchiveData.Configuration;
using ArchiveData.Services;
using System;
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
            servicesSqlServer.Reset(DBConfigEnum.SqlServer);
            WriteLine("**********************SQL SERVER FINISHED************************************");

            WriteLine("********************************MYSQL STARTING********************************");
            //MY SQL - time stamp => long
            TestArchiveService servicesMySql = new TestArchiveService(new MockDataService(), DBConfigEnum.SqlServer);
            await servicesMySql.RunOnlySaveChangesTests();
            await servicesMySql.RunOnlyBulkTests();
            //servicesMySql.Reset(DBConfigEnum.MySql);
            Console.WriteLine("**********************MYSQL FINISHED************************************");
            //PostgresSQL
            Console.WriteLine("*********************POSTGRESQL STARTING********************************");
            TestArchiveService servicesPostgreSql = new TestArchiveService(new MockDataService(), DBConfigEnum.PostgreSql);
            await servicesPostgreSql.RunOnlySaveChangesTests();
            await servicesPostgreSql.RunOnlyBulkTests();
            //servicesPostgreSql.Reset(DBConfigEnum.PostgreSql);


            ReadLine();

        }


    }
}
