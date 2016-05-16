using ConsoleUtil.DependencyResolvers;
using ConsoleUtil.Interfaces;
using Ninject;

namespace ConsoleUtil
{
    public class Program
    {
        public static void Main()
        {
            var kernel = new StandardKernel(new DependencyResolver());

            var mainProcessor = kernel.Get<IMainProcessor>();
            mainProcessor.StartAsync().Wait();
        }
    }
}