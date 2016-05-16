using ConsoleUtil.Interfaces;
using ConsoleUtil.Services;
using ConsoleUtil.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Services
{
    [TestFixture]
    public class FilesPathesServiceTests{

        private const string START_PATH = @"d:\test\one.txt";
        private const string END_OF_PATH = " /";

        [Test]
        public void ReverseFilePath_PerformReverse_ReturnsReversedFIlePath()
        {
            // Arrange
            var stubSvcClass = new TestableFilesPathesService();
            var svcClass = new FilesPathesService();

            var expected = stubSvcClass.ReverseFilePath(START_PATH);
            // Act
            var result = svcClass.ReverseFilePath(START_PATH);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseString_PerformReverse_ReturnsReversedFIlePath()
        {
            // Arrange
            var stubSvcClass = new TestableFilesPathesService();
            var svcClass = new FilesPathesService();

            var expected = stubSvcClass.ReverseString(START_PATH);

            // Act
            var result = svcClass.ReverseString(START_PATH);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AppendAtTheEndOfPath_PerformAppending_ReturnsFilePathWithNewEnd()
        {
            // Arrange
            var stubSvcClass = new TestableFilesPathesService();
            var svcClass = new FilesPathesService();

            var expected = stubSvcClass.AppendAtTheEndOfPath(START_PATH, END_OF_PATH);

            // Act
            var result = svcClass.AppendAtTheEndOfPath(START_PATH, END_OF_PATH);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}