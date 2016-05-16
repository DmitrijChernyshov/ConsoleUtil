using System;
using System.IO;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Operations
{
    public class FilePathesManager : IFilePathesManager
    {
        private const string SEARCH_PATTERN = "*";

        public string[] GetFilesPathes(
            string startPath,
            string searchPattern = SEARCH_PATTERN
            )
        {
            string[] filesPathes = null;

            filesPathes = GetFilesFromDirectory(startPath, searchPattern);
            
            return filesPathes;
        }

        protected virtual string[] GetFilesFromDirectory(
            string startPath,
            string searchPattern = SEARCH_PATTERN)
        {
            string[] result = null;

            try
            {
                result =
                    Directory.GetFiles(
                        startPath,
                        searchPattern,
                        SearchOption.AllDirectories);
            }
            catch (ArgumentException ex)
            {
                // logger
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                // logger
                throw;
            }
            catch (IOException ex)
            {
                // logger
                throw;
            }

            return result;
        }
    }
}