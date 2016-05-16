using System.Threading.Tasks;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using ConsoleUtil.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Operations
{
    [TestFixture]
    public class OperationReversed2Tests
    {
        private const string CORRECT_START_PATH = @"d:\test\start";
        private const string SEARCH_PATTERN = "*";

        private readonly string[] _filePathes =
        {
            @"d:\test\one.txt",
            @"d:\test\two.txt",
            @"d:\test\three.txt"
        };

        private readonly string[] _resultFilePathes =
        {
            @"txt.eno\tset\:d",
            @"txt.owt\tset\:d",
            @"txt.eerht\tset\:d"
        };

        [Test]
        public async Task PerformOperation_CorrectDataPassed_ReturnsFilePathes()
        {
            // Arrange
            var stubBaseClass = new Mock<IFilePathesManager>();

            var stubServicClass =
                new TestableFilesPathesService();

            stubBaseClass
                .Setup(m => m.GetFilesPathes(CORRECT_START_PATH, SEARCH_PATTERN))
                .Returns(_filePathes);

            var operationRev2 = 
                new OperationReversed2(stubBaseClass.Object, stubServicClass);

            // Act
            var result = await operationRev2
                .PerformOperation(CORRECT_START_PATH);

            // Assert
            Assert.AreEqual(_resultFilePathes, result);
        }
    }
}