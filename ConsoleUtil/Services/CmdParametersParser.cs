using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Services
{
    public class CmdParametersParser : IParametersParser
    {
        private const string DEFAULT_RESULT_PATH = @"d:\test\results.txt";

        private const string TXT_FILE_EXTENSION = ".txt";

        private const char SEPARATE_CHAR = ' ';

        private const byte LOWER_BOUND = 2;
        private const byte UPPER_BOUND = 3;

        private const string ALL = "ALL";
        private const string CPP = "CPP";
        private const string REVERSED_1 = "REVERSED1";
        private const string REVERSED_2 = "REVERSED2";

        private readonly Dictionary<string, Operation> _options =
            new Dictionary<string, Operation>()
            {
                { ALL, Operation.All},
                { CPP, Operation.Cpp},
                { REVERSED_1, Operation.Reversed1},
                { REVERSED_2, Operation.Reversed2}
            };
        
        public string StartDirectory { get; private set; }

        public string ResultFilePath { get; private set; }

        public Operation? Option { get; private set; }

        public bool Parse(string lineToParse)
        {
            var isParsed = false;

            if (string.IsNullOrWhiteSpace(lineToParse))
            {
                // logger
            }
            else
            {
                var parameters = SplitParams(lineToParse);

                if (parameters.Length < LOWER_BOUND || parameters.Length > UPPER_BOUND)
                {
                    // logger
                }
                else
                {
                    StartDirectory = ParseStartDirectory(parameters[0]);

                    if (StartDirectory != null)
                    {
                        Option = ParseOperation(parameters[1]);

                        if (Option != null)
                        {
                            isParsed = true;

                            if (parameters.Length == UPPER_BOUND)
                            {
                                var resultFilePath = ParseResultFilePath(parameters[2]);

                                if (!string.IsNullOrWhiteSpace(resultFilePath))
                                {
                                    ResultFilePath = resultFilePath;
                                }
                                else
                                {
                                    isParsed = false;
                                }
                            }
                        }
                    }
                }
            }
            
            return isParsed;
        }

        public virtual string[] SplitParams(string lineToParse)
        {
            var parameters = lineToParse.Split(SEPARATE_CHAR);

            return parameters;
        }

        public virtual string ParseStartDirectory(string startDirectory)
        {
            string parsedDirectory = null;

            if (Directory.Exists(startDirectory))
            {
                parsedDirectory = startDirectory;
            }
            else
            {
                // logger
            }

            return parsedDirectory;
        }

        public virtual Operation? ParseOperation(string operation)
        {
            Operation? option = null;

            if (!string.IsNullOrWhiteSpace(operation))
            {
                var op = operation.ToUpperInvariant();

                if (_options.ContainsKey(op))
                {
                    option = _options[op];
                }
                else
                {
                    // logger
                }
            }
            else
            {
                // logger
            }

            return option;
        }

        public virtual string ParseResultFilePath(string resultFilePath)
        {
            string resultPath = null;

            if (!string.IsNullOrWhiteSpace(resultFilePath))
            {
                var parentDir = GetParentDirectoryByFilePath(resultFilePath);
                
                if (Directory.Exists(parentDir))
                {
                    var fileName = Path.GetFileName(resultFilePath);
                    
                    if (IsCorrectFileName(fileName))
                    {
                        resultPath = resultFilePath;
                    }
                    else
                    {
                        // logger
                    }
                }
                else
                {
                    // logger
                }
            }
            else
            {
                // logger
                resultPath = DEFAULT_RESULT_PATH;
            }

            return resultPath;
        }

        public virtual string GetParentDirectoryByFilePath(string filePath)
        {
            string parentDir = null;

            try
            {
                var parentDirInfo = Directory.GetParent(filePath);
                parentDir = parentDirInfo.FullName;
            }
            catch (ArgumentException ex)
            {
                // logger
            }
            catch (NullReferenceException ex)
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

            return parentDir;
        }

        public virtual bool IsCorrectFileName(string file)
        {
            var result = true;

            var badCharacters =
                new Regex("[" + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]");

            var fileExtension = Path.GetExtension(file);

            var badFile = badCharacters.IsMatch(file);

            var wrongFileExtension = fileExtension != TXT_FILE_EXTENSION;

            if (badFile || wrongFileExtension)
            {
                result = false;
            }

            return result;
        }
    }
}