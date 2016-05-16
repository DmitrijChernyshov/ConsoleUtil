namespace ConsoleUtil.Interfaces
{
    public interface IParametersParser
    {
        string StartDirectory { get; }

        string ResultFilePath { get; }

        Operation? Option { get; }

        bool Parse(string lineToParse);

        string[] SplitParams(string lineToParse);

        string ParseStartDirectory(string startDirectory);

        Operation? ParseOperation(string operation);

        string ParseResultFilePath(string resultFilePath);

        string GetParentDirectoryByFilePath(string filePath);

        bool IsCorrectFileName(string file);
    }
}