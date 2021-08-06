namespace ArchiveData.Tests
{
    public class TestResult
    {
        public double TotalSecondsSpent { get; set; }

        public TestEnum TestEnum { get; set; }

        public TestType TestType { get; set; }

        public TestResult(double totalSecondsSpent, TestEnum testEnum, TestType testType)
        {
            TotalSecondsSpent = totalSecondsSpent;
            TestEnum = testEnum;
            TestType = testType;
        }
    }
}
