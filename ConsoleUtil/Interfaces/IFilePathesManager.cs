namespace ConsoleUtil.Interfaces
{
    public interface IFilePathesManager
    {
        string[] GetFilesPathes(string startPath, string searchPattern);
    }
}