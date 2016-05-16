using ConsoleUtil.Interfaces;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Operations
{
    [TestFixture]
    public class OperationBaseTests
    {
        private const string CORRECT_START_PATH = @"d:\test\start";
        private const string SEARCH_PATTERN = "*";

        private readonly string[] _filePathes =
        {
            @"d:\one.txt",
            @"d:\two.txt",
            @"d:\three.txt"
        };

        [Test]
        public void GetFilesPathes_CorrectDataPassed_ReturnsFilePathes()
        {
            // Arrange
            var stubBaseClass = new Mock<IFilePathesManager>();

            stubBaseClass
                .Setup(m => m.GetFilesPathes(CORRECT_START_PATH, SEARCH_PATTERN))
                .Returns(_filePathes);

            // Act
            var result = stubBaseClass.Object.GetFilesPathes(CORRECT_START_PATH, SEARCH_PATTERN);

            // Assert
            Assert.AreEqual(_filePathes, result);
        }
    }
}