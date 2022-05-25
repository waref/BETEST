using Autofac;
using DataLayer.IServices;
using Engine.IServices;
using Engine.Models;
using LuccaDevises.DataAdapter;
using LuccaDevises.DataAdapterProcess.ResponseModel;
using LuccaDevises.Ioc;
using ServiceLayer.IServices;

namespace LuccaDevises
{

    static  class Programm
    {
        static void Main(string[] args)
        {
            Injector.Inject();

            IFilePathValidator fileValidator = Injector.Container.Resolve<IFilePathValidator>();
            IFileReader fileReader = Injector.Container.Resolve<IFileReader>();
            IFormatValidator<IEnumerable<string>> formatValidator = Injector.Container.Resolve<IFormatValidator<IEnumerable<string>>>();
            IExchangeCalculator<ExchangeToken> calculator = Injector.Container.Resolve<IExchangeCalculator<ExchangeToken>>();

            ConsoleDataAdapterProcess consoleAdapter = new(fileValidator, fileReader, formatValidator, calculator);

            consoleAdapter.Execute(args, out ConsoleResponse response);

            if (response.IsSuceeded)
            {
                Console.WriteLine(response.Exchangeresult.ToString());
            }
            Console.WriteLine(response.ConsoleMessage.ToString());

            Console.WriteLine(" :) ");
        }


    }
}