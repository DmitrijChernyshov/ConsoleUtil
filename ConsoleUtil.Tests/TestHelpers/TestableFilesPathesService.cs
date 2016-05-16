using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Tests.TestHelpers
{
    public class TestableFilesPathesService : IFilePathesService
    {
        private readonly string[] _appended =
        {
            @"d:\test\one.txt /",
            @"d:\test\two.txt /",
            @"d:\test\three.txt /"
        };

        private readonly string[] _reversed =
        {
            @"one.txt\test\d:",
            @"two.txt\test\d:",
            @"three.txt\test\d:"
        };

        private readonly string[] _multipleReversed =
        {
            @"txt.eno\tset\:d",
            @"txt.owt\tset\:d",
            @"txt.eerht\tset\:d"
        };

        private byte _counter;

        public TestableFilesPathesService()
        {
            _counter = 0;
        }

        public string ReverseFilePath(string filePath)
        {
            var result = _reversed[_counter];

            _counter++;

            return result;
        }

        public string ReverseString(string strToReverse)
        {
            var result = _multipleReversed[_counter];

            _counter++;

            return result;
        }

        public string AppendAtTheEndOfPath(string filePath, string endOfFile)
        {
            var result = _appended[_counter];

            _counter++;

            return result;
        }
    }
}