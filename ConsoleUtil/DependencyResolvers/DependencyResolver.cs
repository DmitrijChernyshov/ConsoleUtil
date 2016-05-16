using ConsoleUtil.Interfaces;
using ConsoleUtil.Operations;
using ConsoleUtil.Services;
using Ninject.Modules;

namespace ConsoleUtil.DependencyResolvers
{
    public class DependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainProcessor>().To<MainProcessor>();
            //Bind<IMainProcessor>().ToSelf();
            
            Bind<IConsoleManager>().To<ConsoleManager>();
            Bind<IParametersParser>().To<CmdParametersParser>();
            Bind<IFilePathesManager>().To<FilePathesManager>();
            Bind<IFilePathesService>().To<FilesPathesService>();

            Bind<IOperationFactory>().To<OperationFactory>();

            Bind<IFileManager>().To<FileManager>();

            Bind<IOperation>().To<OperationAll>().Named("All");
            Bind<IOperation>().To<OperationCpp>().Named("Cpp");
            Bind<IOperation>().To<OperationReversed1>().Named("Reversed1");
            Bind<IOperation>().To<OperationReversed2>().Named("Reversed2");

            Bind<OperationAll>().ToSelf();
            Bind<OperationCpp>().ToSelf();
            Bind<OperationReversed1>().ToSelf();
            Bind<OperationReversed2>().ToSelf();
        }
    }
}