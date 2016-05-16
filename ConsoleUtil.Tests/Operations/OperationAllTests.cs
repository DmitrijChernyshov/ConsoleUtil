using System.Threading.Tasks;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Operations
{
    [TestFixture]
    public class OperationAllTests
    {
        private const string CORRECT_START_PATH = @"d:\test\start";
        private const string SEARCH_PATTERN = "*";

        private readonly string[] _resultFilePathes =
        {
            @"d:\one.txt",
            @"d:\two.txt",
            @"d:\three.txt"
        };

        [Test]
        public async Task PerformOperation_CorrectDataPassed_ReturnsFilePathes()
        {
            // Arrange
            var stubBaseClass = new Mock<IFilePathesManager>();

            stubBaseClass
                .Setup(m => m.GetFilesPathes(CORRECT_START_PATH, SEARCH_PATTERN))
                .Returns(_resultFilePathes);

            var operationAll = new OperationAll(stubBaseClass.Object);
            
            // Act
            var result = await operationAll
                .PerformOperation(CORRECT_START_PATH);

            // Assert
            Assert.AreEqual(_resultFilePathes, result);
        }
    }
}