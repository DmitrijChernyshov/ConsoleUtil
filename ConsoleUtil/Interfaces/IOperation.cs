using System.Threading.Tasks;

namespace ConsoleUtil.Interfaces
{
    public interface IOperation
    {
        Task<string[]> PerformOperation(string startPath);
    }
}