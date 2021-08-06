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
            TestArchiveService services = new TestArchiveService(new MockDataService());
            services.RunOnlySaveChangesTests(db);
            //services.Reset(db);
            
           
            ReadLine();

        }
        
        
    }
}
