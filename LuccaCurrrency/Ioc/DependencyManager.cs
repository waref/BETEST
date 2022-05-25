using Autofac;

namespace LuccaCurrrency.Ioc
{
    public class DependencyManager
    {
        public readonly ContainerBuilder ContainerBuilder;
        public DependencyManager()
        {
            ContainerBuilder = new ContainerBuilder();
            RegisterInjection();
        }
        private static void RegisterInjection()
        {
            ContainerBuilder.RegisterType<FileReader>.As<IFileReader>();
        }
    }
}
