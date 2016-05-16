using System;
using System.Linq;
using System.Text;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Services
{
    public class FilesPathesService : IFilePathesService
    {
        private const char PATH_DIVIDER = '\\';
        
        public string ReverseFilePath(string filePath)
        {
            var reversedSegments = filePath.Split(PATH_DIVIDER).Reverse().ToArray();

            var reversedPath = new StringBuilder();

            short i = 1;

            foreach (var segment in reversedSegments)
            {
                reversedPath.Append(segment);

                if (reversedSegments.Length > i)
                {
                    reversedPath.Append(PATH_DIVIDER);
                }

                i++;
            }

            return reversedPath.ToString();
        }
        
        public string ReverseString(string strToReverse)
        {
            var charArray = strToReverse.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }
        
        public string AppendAtTheEndOfPath(string filePath, string endOfFile)
        {
            return filePath + endOfFile;
        }
    }
}