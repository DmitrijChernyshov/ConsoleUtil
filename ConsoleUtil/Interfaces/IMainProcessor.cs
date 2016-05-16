using System.Threading.Tasks;

namespace ConsoleUtil.Interfaces
{
    public interface IMainProcessor
    {
        Task StartAsync();

        bool ProcessWrittingResult(string[] filesdata);

        bool Exit();
    }
}