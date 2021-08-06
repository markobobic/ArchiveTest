using ArchiveData.Extensions;
using ArchiveData.Tests;
using System;
using static System.Console;

namespace ArchiveData.UI
{
    public static class TestUI
    {
        public static void ShowMeasurementsResults(TestResult result)
        {
            Action description = TestType.OnlySaveChanges == result.TestType ?
                new Action(() =>
                {
                    DescriptionSaveChanges(result.TestEnum);
                }) : new Action(() => DescriptionBulkInsert(result.TestEnum));

            description();
            WriteLine($"For test {result.TestEnum} type {result.TestType} avg seconds spent {result.TotalSecondsSpent}");
            WriteLine($"===============>END OF TEST:{result.TestEnum.ToString().ToUpper()}<===================");
            WriteLine();
            
        }
        public static void WriteOrdinalIterations(int i,TimeSpan ts)
        {
            Console.WriteLine($"{i.ToOrdinalNumbers()} iteration: {ts}");
        }
        public static void ShowStartOfTest(TestConfig config)
        {
            WriteLine($"===============>START OF:{config.TestEnum.ToString().ToUpper()}<===================");

        }
        private static void DescriptionBulkInsert(TestEnum testEnum)
        {
            switch (testEnum)
            {
                case TestEnum.Test1:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 10 000 000");
                    WriteLine("---------------------------------------------");

                    break;
                case TestEnum.Test2:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 10 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test3:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 5 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test4:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 5 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test5:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 1 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test6:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 1 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                default:
                    break;
            }
        }

        private static void DescriptionSaveChanges(TestEnum testEnum)
        {
            switch (testEnum)
            {
                case TestEnum.Test1:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 10 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 10 010 000");
                    WriteLine("We archive: 2 000 000 ");
                    WriteLine("---------------------------------------------");

                    break;
                case TestEnum.Test2:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 10 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 10 010 000");
                    WriteLine("We archive: 200 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test3:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 5 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 5 010 000");
                    WriteLine("We archive: 1 000 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test4:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 5 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 5 010 000");
                    WriteLine("We archive: 200 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test5:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 1 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 1 010 000");
                    WriteLine("We archive: 500 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test6:
                    WriteLine("-------------SPECIFICATION---------------------");
                    WriteLine("BulkInsert initial records: 1 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 1 010 000");
                    WriteLine("We archive: 200 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                default:
                    break;
            }
        }
    }
}
