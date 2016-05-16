namespace ConsoleUtil.Interfaces
{
    public interface IOperationFactory
    {
        IOperation CreateFileOperation(Operation? operation);
    }
}