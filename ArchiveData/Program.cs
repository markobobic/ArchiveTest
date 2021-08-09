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
            
          
            TestArchiveService services = new TestArchiveService(new MockDataService(), DBConfigEnum.MySql);
            await services.RunOnlySaveChangesTests();
            await services.RunOnlyBulkTests();
            services.Reset();

            ReadLine();

        }
        
        
    }
}
