using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiveData.Tests
{
    public class TestConfig
    {
        public int BulkInsertMax { get; set; }
        public int ArchiveLimit { get; set; }
        public TestEnum TestEnum { get; set; }
        public TestType TestType { get; set; }
        public int AdditionalInserts { get; set; }


        public TestConfig(int bulkInsertMax,int additionalInserts,int archiveLimit,TestEnum testEnum,TestType testType)
        {
            BulkInsertMax = bulkInsertMax;
            AdditionalInserts = additionalInserts;
            ArchiveLimit = archiveLimit;
            TestEnum = testEnum;
            TestType = testType;
        }
        public TestConfig(int bulkInsertMax,TestEnum testEnum,TestType testType)
        {
            BulkInsertMax = bulkInsertMax;
            TestEnum = testEnum;
            TestType = testType;
        }
    }
}
