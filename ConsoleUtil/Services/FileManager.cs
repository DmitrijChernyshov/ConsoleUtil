using System;
using System.Collections.Generic;
using System.IO;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Services
{
    public class FileManager : IFileManager
    {
        private const string CORRECT_RESULT_PATH = @"d:\test\results.txt";

        public bool WriteToFile(IEnumerable<string> dataToWrite, string filePath)
        {
            var writtingResult = false;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = CORRECT_RESULT_PATH;
            }

            try
            {
                File.WriteAllLines(filePath, dataToWrite);
                writtingResult = true;
            }
            catch (DirectoryNotFoundException ex)
            {
                // logger
            }
            catch (ArgumentException ex)
            {
                // logger
            }
            catch (UnauthorizedAccessException ex)
            {
                // logger
            }
            catch (IOException ex)
            {
                // logger
            }

            return writtingResult;
        }
    }
}