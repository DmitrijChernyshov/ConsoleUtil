using System.Collections.Generic;

namespace ConsoleUtil.Interfaces
{
    public interface IFileManager
    {
        bool WriteToFile(IEnumerable<string> dataToWrite, string filePath);
    }
}