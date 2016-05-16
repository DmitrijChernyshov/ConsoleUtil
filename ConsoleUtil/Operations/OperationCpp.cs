using System.Linq;
using System.Threading.Tasks;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Operations
{
    public class OperationCpp : IOperation
    {
        private const string SEARCH_PATTERN = "*.cpp";
        private const string END_OF_PATH = " /";
        
        private readonly IFilePathesManager _filePathesManager;
        private readonly IFilePathesService _filePathesService;

        public OperationCpp(
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
                    .Select(filePath =>
                        _filePathesService.AppendAtTheEndOfPath(filePath, END_OF_PATH))
                    .ToArray();

                return result;
            });

            return resultTask;
        }
    }
}