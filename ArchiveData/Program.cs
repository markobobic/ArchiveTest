using ArchiveData.Configuration;
using ArchiveData.DB;
using ArchiveData.Services;
using System.Threading.Tasks;
using static System.Console;

namespace ArchiveData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            // SQL SERVER
            TestArchiveService servicesSqlServer = new TestArchiveService(new MockDataService(), DBConfigEnum.SqlServer);
            await servicesSqlServer.RunOnlySaveChangesTests();
            await servicesSqlServer.RunOnlyBulkTests();
            
            //MY SQL
            TestArchiveService servicesMySql = new TestArchiveService(new MockDataService(), DBConfigEnum.SqlServer);
            await servicesMySql.RunOnlySaveChangesTests();
            await servicesMySql.RunOnlyBulkTests();
           
            ReadLine();

        }
        
        
    }
}
