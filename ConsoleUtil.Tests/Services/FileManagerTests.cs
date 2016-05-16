using System;
using System.IO;
using ConsoleUtil.Interfaces;
using Moq;
using NUnit.Framework;

namespace ConsoleUtil.Tests.Services
{
    [TestFixture]
    public class FileManagerTests
    {
        private const string CORRECT_RESULT_PATH = @"d:\test\results.txt";

        private const string INCORRECT_DIRECTORY = @"d:\asd\results.txt";

        private readonly string[] _dataToWrite =
        {
            @"d:\test\one.txt",
            @"d:\test\two.txt",
            @"d:\test\three.txt"
        };

        [Test]
        public void WriteToFile_CorrectParameters_ReturnsTrue()
        {
            // Arrange
            var writer = new Mock<IFileManager>();

            writer
                .Setup(m => m.WriteToFile(_dataToWrite, CORRECT_RESULT_PATH))
                .Returns(true);

            // Act
            var result =
                writer.Object.WriteToFile(
                    _dataToWrite,
                    CORRECT_RESULT_PATH);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void WriteToFile_WrongDirectory_ExceptionThrown()
        {
            // Arrange
            var writer = new Mock<IFileManager>();

            writer
                .Setup(m => m.WriteToFile(_dataToWrite, INCORRECT_DIRECTORY))
                .Throws<DirectoryNotFoundException>();

            // Act
            var ex = Assert.Catch<DirectoryNotFoundException>(() =>
                writer.Object.WriteToFile(_dataToWrite, INCORRECT_DIRECTORY));
            
            // Assert
            StringAssert.Contains((new DirectoryNotFoundException()).Message, ex.Message);
        }

        [Test]
        public void WriteToFile_NullDataToWrite_ExceptionThrown()
        {
            // Arrange
            var writer = new Mock<IFileManager>();

            string[] nullDataToWrite = null;

            writer
                .Setup(m => m.WriteToFile(nullDataToWrite, CORRECT_RESULT_PATH))
                .Throws<ArgumentException>();

            // Act
            var ex = Assert.Catch<ArgumentException>(() =>
                writer.Object.WriteToFile(nullDataToWrite, CORRECT_RESULT_PATH));

            // Assert
            StringAssert.Contains((new ArgumentException()).Message, ex.Message);
        }

        [Test]
        public void WriteToFile_UnauthorizedAccessToResultFile_ExceptionThrown()
        {
            // Arrange
            var writer = new Mock<IFileManager>();

            writer
                .Setup(m => m.WriteToFile(_dataToWrite, CORRECT_RESULT_PATH))
                .Throws<UnauthorizedAccessException>();

            // Act
            var ex = Assert.Catch<UnauthorizedAccessException>(() =>
                writer.Object.WriteToFile(_dataToWrite, CORRECT_RESULT_PATH));

            // Assert
            StringAssert.Contains((new UnauthorizedAccessException()).Message, ex.Message);
        }

        [Test]
        public void WriteToFile_InputOutputWrongOperation_ExceptionThrown()
        {
            // Arrange
            var writer = new Mock<IFileManager>();

            writer
                .Setup(m => m.WriteToFile(_dataToWrite, CORRECT_RESULT_PATH))
                .Throws<IOException>();

            // Act
            var ex = Assert.Catch<IOException>(() =>
                writer.Object.WriteToFile(_dataToWrite, CORRECT_RESULT_PATH));

            // Assert
            StringAssert.Contains((new IOException()).Message, ex.Message);
        }
    }
}