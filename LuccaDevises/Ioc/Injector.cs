using Autofac;
using DataLayer.IServices;
using DataLayer.Services;
using Engine.IServices;
using Engine.Models;
using Engine.Services;
using ServiceLayer.IServices;
using ServiceLayer.Services;

namespace LuccaDevises.Ioc
{
    public static class Injector
    {
        public static IContainer? Container { get; set; }
        private static ContainerBuilder? ContainerBuilder { get; set; }

        public static void Inject()
        {
            ContainerBuilder = new();
            Register();
            Container = ContainerBuilder.Build();
        }
        private static void Register()
        {

            ContainerBuilder?.RegisterType<FilePathValidator>().As<IFilePathValidator>();
            ContainerBuilder?.RegisterType<FileReader>().As<IFileReader>();
            ContainerBuilder?.RegisterType<StringsFormatValidatorProcess>().As<IFormatValidator<IEnumerable<string>>>();
            ContainerBuilder?.RegisterType<ExchangeCalculatorProcess>().As<IExchangeCalculator<ExchangeToken>>();

        }
    }
}
