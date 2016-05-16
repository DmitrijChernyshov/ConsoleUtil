using ConsoleUtil.Services;

namespace ConsoleUtil.Tests.TestHelpers
{
    public class TestableCmdParametersParser : CmdParametersParser
    {
        public string[] TestSplittedParams { get; set; }
        public string TestStartDirectory { get; set; }
        public string TestResultFilePath { get; set; }
        public Operation? TestOperation { get; set; }
        public bool TestIsCorrectFileName { get; set; }

        public override string[] SplitParams(string lineToParse)
        {
            return TestSplittedParams;
        }

        public override string ParseStartDirectory(string startDirectory)
        {
            return TestStartDirectory;
        }

        public override Operation? ParseOperation(string operation)
        {
            return TestOperation;
        }

        public override string ParseResultFilePath(string resultFilePath)
        {
            return TestResultFilePath;
        }

        public override string GetParentDirectoryByFilePath(string filePath)
        {
            return TestStartDirectory;
        }

        public override bool IsCorrectFileName(string file)
        {
            return TestIsCorrectFileName;
        }
    }
}