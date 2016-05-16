namespace ConsoleUtil.Interfaces
{
    public interface IConsoleManager
    {
        void Write(string inputString);

        void WriteLine(string inputString);

        void WriteNewBlankLine();

        string ReadLine();
    }
}