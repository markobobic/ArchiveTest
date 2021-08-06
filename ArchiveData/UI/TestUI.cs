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
            WriteLine();
            
        }

        private static void DescriptionBulkInsert(TestEnum testEnum)
        {
            switch (testEnum)
            {
                case TestEnum.Test1:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 10 000 000");
                    WriteLine("---------------------------------------------");

                    break;
                case TestEnum.Test2:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 10 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test3:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 5 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test4:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 5 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test5:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 1 000 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test6:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 1 000 000");
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
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 10 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 10 010 000");
                    WriteLine("We archive: 2 000 000 ");
                    WriteLine("---------------------------------------------");

                    break;
                case TestEnum.Test2:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 10 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 10 010 000");
                    WriteLine("We archive: 200 000");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test3:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 5 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 5 010 000");
                    WriteLine("We archive: 1 000 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test4:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 5 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 5 010 000");
                    WriteLine("We archive: 200 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test5:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 1 000 000");
                    WriteLine("Additional records: 10 500");
                    WriteLine("Limit after we archiving: 1 010 000");
                    WriteLine("We archive: 500 000 ");
                    WriteLine("---------------------------------------------");
                    break;
                case TestEnum.Test6:
                    WriteLine("-------------DESCRIPTION---------------------");
                    WriteLine("BulkInsert: 1 000 000");
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
