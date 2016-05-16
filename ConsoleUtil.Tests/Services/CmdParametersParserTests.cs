using System;
using System.IO;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Services
{
    [TestFixture]
    public class CmdParametersParserTests
    {
        private const string TEST_DIRECTORY = @"d:\test";
        private const string DEFAULT_RESULT_FILE_PATH = @"d:\test\results.txt";

        private const string EMPTY_RESULT_FILE_PATH = "";

        private const string INCORRECT_DIRECTORY = @"d:\asd\result.txt";
        private const string INCORRECT_FILE = @"d:\test\asd.cs";

        private const string CORRECT_FILE_NAME = "result.txt";
        private const string INCORRECT_FILE_NAME = "result.cs";
        private const string FILE_WITHOUT_EXT = "result";

        private const string OPERATION_ALL = "all";
        private const string INCORRECT_OPERATION = "asd";

        private const string CORRECT_PARAMS_LINE = @"d:\test\start all d:\test\results.txt";


        private string[] _splittedParams = 
        {
            @"d:\test\start",
            "all",
            @"d:\test\results.txt"
        };

        [Test]
        public void Parse_CorrectCmdLinePassed_ReturnsTrue()
        {
            // Arrange
            var stubParser = new TestableCmdParametersParser();

            stubParser.TestSplittedParams = _splittedParams;
            stubParser.TestStartDirectory = TEST_DIRECTORY;
            stubParser.TestResultFilePath = DEFAULT_RESULT_FILE_PATH;
            stubParser.TestOperation = Operation.All;
            stubParser.TestIsCorrectFileName = true;

            // Act
            var isParsed = stubParser.Parse(CORRECT_PARAMS_LINE);

            // Assert
            Assert.IsTrue(isParsed);
        }

        [Test]
        public void SplitParams_CorrectCmdLinePassed_CorrectSplittedParam()
        {
            // Arrange
            var stubParser = new TestableCmdParametersParser();

            stubParser.TestSplittedParams = _splittedParams;

            // Act
            var splitResult = stubParser.SplitParams(CORRECT_PARAMS_LINE);

            // Assert
            Assert.AreEqual(3, splitResult.Length);
        }

        [TestCase(TEST_DIRECTORY)]
        [TestCase(null)]
        public void ParseStartDirectory_StartDirectoryPassed_EqualToExpected(
            string expectedResult)
        {
            // Arrange
            var stubParser = new TestableCmdParametersParser();

            stubParser.TestStartDirectory = expectedResult;

            // Act
            var parseResult = stubParser.ParseStartDirectory(expectedResult);

            // Assert
            Assert.AreEqual(expectedResult, parseResult);
        }

        [TestCase(OPERATION_ALL, Operation.All)]
        [TestCase(INCORRECT_OPERATION, null)]
        public void ParseOperation_OperationPassed_EqualToExpected(
            string operationPassed,
            Operation? expectedResult)
        {
            // Arrange
            var stubParser = new TestableCmdParametersParser();

            stubParser.TestOperation = expectedResult;

            // Act
            var parseResult = stubParser.ParseOperation(operationPassed);

            // Assert
            Assert.AreEqual(expectedResult, parseResult);
        }

        [Test]
        public void GetParentDirectoryByFilePath_CorrectFilePathPassed_ParentDirectoryReturns()
        {
            // Arrange
            var stubParser = new TestableCmdParametersParser();

            stubParser.TestStartDirectory = TEST_DIRECTORY;

            // Act
            var parentDir = stubParser.GetParentDirectoryByFilePath(DEFAULT_RESULT_FILE_PATH);

            // Assert
            Assert.AreEqual(TEST_DIRECTORY, parentDir);
        }

        [Test]
        public void GetParentDirectoryByFilePath_DirectoryDoesNotExist_ExceptionThrown()
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.GetParentDirectoryByFilePath(INCORRECT_DIRECTORY))
                .Throws<DirectoryNotFoundException>();

            // Act
            var e = Assert.Catch<DirectoryNotFoundException>(() =>
                stubParser.Object.GetParentDirectoryByFilePath(INCORRECT_DIRECTORY));

            // Assert
            StringAssert.Contains((new DirectoryNotFoundException()).Message, e.Message);
        }

        [Test]
        public void GetParentDirectoryByFilePath_IncorrectFile_ExceptionThrown()
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.GetParentDirectoryByFilePath(INCORRECT_FILE))
                .Throws<ArgumentException>();

            // Act
            var e = Assert.Catch<ArgumentException>(() =>
                stubParser.Object.GetParentDirectoryByFilePath(INCORRECT_FILE));

            // Assert
            StringAssert.Contains((new ArgumentException()).Message, e.Message);
        }

        [TestCase(CORRECT_FILE_NAME, true)]
        [TestCase(INCORRECT_FILE_NAME, false)]
        public void IsCorrectFileName_FileNamePassed_ReturnsExpected(
            string fileName,
            bool expectedResult)
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.IsCorrectFileName(fileName))
                .Returns(expectedResult);

            // Act
            var result = stubParser.Object.IsCorrectFileName(fileName);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void IsCorrectFileName_FileWithoutExtension_ExceptionThrown()
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.IsCorrectFileName(FILE_WITHOUT_EXT))
                .Throws<ArgumentException>();

            // Act
            var e = Assert.Catch<ArgumentException>(() =>
                stubParser.Object.IsCorrectFileName(FILE_WITHOUT_EXT));

            // Assert
            StringAssert.Contains((new ArgumentException()).Message, e.Message);
        }

        [Test]
        public void ParseResultFilePath_ResultFilePathPassed_ReturnsResultFilePath()
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.GetParentDirectoryByFilePath(DEFAULT_RESULT_FILE_PATH))
                .Returns(TEST_DIRECTORY);

            stubParser
                .Setup(m => m.IsCorrectFileName(CORRECT_FILE_NAME))
                .Returns(true);

            stubParser
                .Setup(m => m.ParseResultFilePath(DEFAULT_RESULT_FILE_PATH))
                .Returns(DEFAULT_RESULT_FILE_PATH);

            // Act
            var result = stubParser.Object.ParseResultFilePath(DEFAULT_RESULT_FILE_PATH);

            // Assert
            Assert.AreEqual(DEFAULT_RESULT_FILE_PATH, result);
        }

        [Test]
        public void ParseResultFilePath_ResultFilePathPassed_ReturnsDefaultFilePath()
        {
            // Arrange
            var stubParser = new Mock<IParametersParser>();
            
            stubParser
                .Setup(m => m.ParseResultFilePath(EMPTY_RESULT_FILE_PATH))
                .Returns(DEFAULT_RESULT_FILE_PATH);

            // Act
            var result = stubParser.Object.ParseResultFilePath(EMPTY_RESULT_FILE_PATH);

            // Assert
            Assert.AreEqual(DEFAULT_RESULT_FILE_PATH, result);
        }
    }
}