using System.Threading.Tasks;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Operations
{
    public class OperationAll : IOperation
    {
        private const string SEARCH_PATTERN = "*";

        private readonly IFilePathesManager _filePathesManager;

        public OperationAll(IFilePathesManager filePathesManager)
        {
            _filePathesManager = filePathesManager;
        }

        public Task<string[]> PerformOperation(string startPath)
        {
            var result = Task.Run(() => 
                    _filePathesManager.GetFilesPathes(startPath, SEARCH_PATTERN));

            return result;
        }
    }
}