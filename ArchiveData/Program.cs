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

            using var db = new ApplicationDBContext();
            TestArchiveService services = new TestArchiveService(new MockDataService());
            services.RunOnlySaveChangesTests(db);
            services.RunOnlyBulkTests(db);
            //services.Reset(db);
            
           
            ReadLine();

        }
        
        
    }
}
