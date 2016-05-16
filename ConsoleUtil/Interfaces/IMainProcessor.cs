using System.Threading.Tasks;

namespace ConsoleUtil.Interfaces
{
    public interface IMainProcessor
    {
        Task StartAsync();

        Task<bool> ProcessWrittingResultAsync(Task<string[]> filesdata);

        bool Exit();
    }
}