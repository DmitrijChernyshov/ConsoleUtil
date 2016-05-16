using ConsoleUtil.Interfaces;
using Ninject;

namespace ConsoleUtil
{
    public class OperationFactory : IOperationFactory
    {
        private const string ALL = "All";
        private const string CPP = "Cpp";
        private const string REVERSED_1 = "Reversed1";
        private const string REVERSED_2 = "Reversed2";

        private readonly IKernel _kernel;

        public OperationFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IOperation CreateFileOperation(Operation? operation)
        {
            IOperation pickedOperation = null;

            if (operation == null)
            {
                // logger
            }
            else
            {
                switch (operation)
                {
                    case Operation.All:
                        {
                            pickedOperation = _kernel.Get<IOperation>(ALL);
                        }
                        break;
                    case Operation.Cpp:
                        {
                            pickedOperation = _kernel.Get<IOperation>(CPP);
                        }
                        break;
                    case Operation.Reversed1:
                        {
                            pickedOperation = _kernel.Get<IOperation>(REVERSED_1);
                        }
                        break;
                    case Operation.Reversed2:
                        {
                            pickedOperation = _kernel.Get<IOperation>(REVERSED_2);
                        }
                        break;
                }
            }

            return pickedOperation;
        }
    }
}