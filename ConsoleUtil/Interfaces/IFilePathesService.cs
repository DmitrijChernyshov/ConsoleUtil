namespace ConsoleUtil.Interfaces
{
    public interface IFilePathesService
    {
        string ReverseFilePath(string filePath);

        string ReverseString(string strToReverse);

        string AppendAtTheEndOfPath(string filePath, string endOfFile);
    }
}