using System.Linq;
using System.Threading.Tasks;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Operations
{
    public class OperationReversed1 : IOperation
    {
        private const string SEARCH_PATTERN = "*";

        private readonly IFilePathesManager _filePathesManager;
        private readonly IFilePathesService _filePathesService;

        public OperationReversed1(
            IFilePathesManager filePathesManager,
            IFilePathesService filePathesService)
        {
            _filePathesManager = filePathesManager;
            _filePathesService = filePathesService;
        }

        public Task<string[]> PerformOperation(string startPath)
        {
            var resultTask = Task.Run(() =>
            {
                var filePathes =
                    _filePathesManager.GetFilesPathes(startPath, SEARCH_PATTERN);

                var result = filePathes
                    .Select(filePath => _filePathesService.ReverseFilePath(filePath))
                    .ToArray();

                return result;
            });

            return resultTask;
        }
    }
}