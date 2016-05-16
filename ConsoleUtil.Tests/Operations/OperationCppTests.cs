using System.Threading.Tasks;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using ConsoleUtil.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Operations
{
    [TestFixture]
    public class OperationCppTests
    {
        private const string CORRECT_START_PATH = @"d:\test\start";
        private const string SEARCH_PATTERN = "*.cpp";

        private readonly string[] _filePathes =
        {
            @"d:\test\one.txt",
            @"d:\test\two.txt",
            @"d:\test\three.txt"
        };

        private readonly string[] _resultFilePathes =
        {
            @"d:\test\one.txt /",
            @"d:\test\two.txt /",
            @"d:\test\three.txt /"
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
            
            var operationCpp = new OperationCpp(stubBaseClass.Object, stubServicClass);
            
            // Act
            var result = await operationCpp
                .PerformOperation(CORRECT_START_PATH);

            // Assert
            Assert.AreEqual(_resultFilePathes, result);
        }
    }
}