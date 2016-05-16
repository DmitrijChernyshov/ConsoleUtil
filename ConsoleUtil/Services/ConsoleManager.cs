using System;
using ConsoleUtil.Interfaces;

namespace ConsoleUtil.Services
{
    public class ConsoleManager : IConsoleManager
    {
        public void Write(string inputString)
        {
            Console.Write(inputString);
        }

        public void WriteLine(string inputString = "")
        {
            Console.WriteLine(inputString);
        }

        public void WriteNewBlankLine()
        {
            Console.WriteLine();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}