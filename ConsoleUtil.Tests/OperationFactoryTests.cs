using ConsoleUtil.DependencyResolvers;
using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using Moq;
using Ninject;
using NUnit.Framework;

namespace ConsoleUtil.Tests
{
    [TestFixture]
    public class OperationFactoryTests
    {
        private const string OPERATION_CPP = "Cpp";
        private Operation _operationToCreate = Operation.Cpp;

        [Test]
        public void CreateFileOperation_OperationPassed_ReturnsFileOperationNewInstance()
        {
            // Arrange
            var standardKernel = new StandardKernel(new DependencyResolver());

            var expectedOperationType = typeof(OperationCpp);

            var factory = new OperationFactory(standardKernel);

            // Act
            var resultFileOperation =
                factory.CreateFileOperation(_operationToCreate)
                .GetType();

            // Assert
            Assert.AreEqual(expectedOperationType, resultFileOperation);
        }
    }
}