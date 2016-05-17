using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUtil.DependencyResolvers;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using NUnit.Framework;

namespace ConsoleUtil.Tests
{
    [TestFixture]
    public class MainProcessorTests
    {
        private const string CPP_OPERATION = "Cpp";
        private const string EXIT_CONDITION = "q";
        
        private const string CORRECT_START_PATH = @"d:\test\start";
        private const string CORRECT_RESULT_FILE_PATH = @"d:\test\results.txt";

        private readonly string[] _resultFilePathes =
        {
            @"d:\one.txt",
            @"d:\two.txt",
            @"d:\three.txt"
        };

        private Operation _correctOperation = Operation.Cpp;

        private IKernel _kernel;

        private Mock<IConsoleManager>   _stubConsoleManger;
        private Mock<IParametersParser> _stubParser;
        private Mock<IOperationFactory> _stubFactory;
        private Mock<IOperation>        _stubOperation;
        private Mock<IFileManager>      _stubFileWriter;

        [SetUp]
        public void Initial()
        {
            _kernel = new StandardKernel(new DependencyResolver());

            _stubConsoleManger = StubConsoleManger();
            _stubParser = StubParametersParser();
            _stubFactory = StubOperationFactory();
            _stubOperation = StubOperation();
            _stubFileWriter = StubFileWriter();
        }

        [Test]
        public async Task StartAsync_CorrectParametersPassed_OperationSuccessfullyPerformed()
        {
            // Arrange

            var mainProcessor = 
                new MainProcessor(
                    _stubConsoleManger.Object,
                    _stubParser.Object,
                    _stubFactory.Object,
                    _stubFileWriter.Object);

            mainProcessor.FileOperation = _stubOperation.Object;

            // Act
            await mainProcessor.StartAsync();

            // Assert
            _stubConsoleManger.Verify(scm => scm.ReadLine(), Times.Exactly(2));

            _stubParser.Verify(sp => sp.Parse(It.IsAny<string>()), Times.Once);

            _stubOperation.Verify(so => so.PerformOperation(It.IsAny<string>()),
                Times.Once);

            _stubFileWriter.Verify(sfw => 
                sfw.WriteToFile(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()),
                Times.Once);
        }

        #region TestHelpers
        private Mock<IConsoleManager> StubConsoleManger()
        {
            var stubConsoleManager = new Mock<IConsoleManager>();

            // Returns EXIT_CONDITION because in the first call we don't care 
            // what _console.ReadLine() returns and the second call gets StartAsync
            // exited correctly
            stubConsoleManager
                .Setup(m => m.ReadLine())
                .Returns(EXIT_CONDITION);

            return stubConsoleManager;
        }

        private Mock<IParametersParser> StubParametersParser(
            bool parse = true,
            string startPath = CORRECT_START_PATH,
            Operation? option = Operation.Cpp,
            string resultFilePath = CORRECT_RESULT_FILE_PATH)
        {
            var stubParser = new Mock<IParametersParser>();

            stubParser
                .Setup(m => m.Parse(It.IsAny<string>()))
                .Returns(parse);

            stubParser
                .SetupGet(p => p.StartDirectory)
                .Returns(startPath);

            stubParser
                .SetupGet(p => p.Option)
                .Returns(option);

            stubParser
                .SetupGet(p => p.ResultFilePath)
                .Returns(resultFilePath);

            return stubParser;
        }

        private Mock<IOperationFactory> StubOperationFactory(string operation = CPP_OPERATION)
        {
            var stubFactory = new Mock<IOperationFactory>();

            stubFactory
                .Setup(m => m.CreateFileOperation(It.IsAny<Operation>()))
                .Returns(_kernel.Get<IOperation>(operation));

            return stubFactory;
        }

        private Mock<IOperation> StubOperation()
        {
            var stubOperation = new Mock<IOperation>();

            stubOperation
                .Setup(m => m.PerformOperation(It.IsAny<string>()))
                .Returns(Task.FromResult(_resultFilePathes));

            return stubOperation;
        }

        private Mock<IFileManager> StubFileWriter()
        {
            var stubFileManager = new Mock<IFileManager>();

            stubFileManager
                .Setup(m => m.WriteToFile(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()))
                .Returns(true);

            return stubFileManager;
        }
        #endregion

    }
}