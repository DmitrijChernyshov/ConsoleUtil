﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil
{
    public class MainProcessor : IMainProcessor
    {
        private const string PROGRESS_UNIT = ".";
        private const string EXIT_CONDITION = "Q";

        private readonly IConsoleManager _console;
        private readonly IParametersParser _parser;
        private readonly IOperationFactory _factory;
        private readonly IFileManager _fileManager;

        public IOperation FileOperation { get; set; }

        public MainProcessor(
            IConsoleManager console,
            IParametersParser parser,
            IOperationFactory factory,
            IFileManager fileManager)
        {
            _console = console;
            _parser = parser;
            _factory = factory;
            _fileManager = fileManager;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                _console.Write("consoleUtil.exe ");

                var userInput = _console.ReadLine();

                var parseResult = _parser.Parse(userInput);

                if (parseResult)
                {
                    if (FileOperation == null)
                    {
                        FileOperation = _factory.CreateFileOperation(_parser.Option);
                    }

                    if (FileOperation != null)
                    {
                        var taskResult = FileOperation.PerformOperation(_parser.StartDirectory);

                        if (ProcessFileOperation(taskResult))
                        {
                            await ProcessWrittingResultAsync(taskResult);
                        }
                    }
                    else
                    {
                        _console.Write("Error has been occured. Check log file for details.");
                        // logger
                    }
                }
                else
                {
                    _console.Write("Error has been occured. Check log file for details.");
                    // logger
                }

                if (Exit())
                    break;
            }
        }

        public bool ProcessFileOperation(Task<string[]> taskResult)
        {
            var fileProcessingResult = false;

            if (taskResult.Exception == null)
            {
                _console.Write("Files processing...");

                while (!taskResult.IsCompleted)
                {
                    _console.Write(PROGRESS_UNIT);
                    Thread.Sleep(200);
                }

                _console.WriteNewBlankLine();
                _console.Write("Done");
                _console.WriteNewBlankLine();
                fileProcessingResult = true;
            }
            else
            {
                _console.Write("Error has been occured. Check log file for details.");
                // logger
            }

            return fileProcessingResult;
        }
        
        public async Task<bool> ProcessWrittingResultAsync(Task<string[]> filesdata)
        {
            var writtingResult = false;

            _console.Write("Writting files pathes into result file...");

            try
            {
                writtingResult = _fileManager.WriteToFile(
                    await filesdata,
                    _parser.ResultFilePath);
            }
            catch (Exception ex)
            {
                _console.Write("Error has been occured. Check log file for details.");
                // logger
            }

            return writtingResult;
        }

        public bool Exit()
        {
            var result = false;

            while (true)
            {
                _console.WriteNewBlankLine();
                _console.WriteLine("Type q or Q for exit or press Enter to continue.");
                var answer = _console.ReadLine();

                if (!string.IsNullOrWhiteSpace(answer))
                {
                    answer = answer.ToUpperInvariant();
                }

                if (answer == EXIT_CONDITION)
                {
                    result = true;
                    break;
                }

                if (answer == string.Empty)
                {
                    break;
                }
            }

            return result;
        }
    }
}