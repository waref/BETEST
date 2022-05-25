using DataLayer.IServices;
using Engine.IServices;
using Engine.Models;
using LuccaDevises.DataAdapterProcess.Concretes;
using LuccaDevises.DataAdapterProcess.ResponseModel;
using ServiceLayer.IServices;


namespace LuccaDevises.DataAdapter
{
    public class ConsoleDataAdapterProcess
    {
        private readonly IFilePathValidator _FilePathValidator;
        private readonly IFileReader _FileReader;
        private readonly IFormatValidator<IEnumerable<string>> _FormatValidator;
        private readonly IExchangeCalculator<ExchangeToken> _ExchangeCalculator;

        public ConsoleDataAdapterProcess(IFilePathValidator fileValidator, IFileReader fileReader, IFormatValidator<IEnumerable<string>> formatValidator, IExchangeCalculator<ExchangeToken> exchangeCalculator)
        {
            _FilePathValidator = fileValidator;
            _FileReader = fileReader;
            _FormatValidator = formatValidator;
            _ExchangeCalculator = exchangeCalculator;
        }

        public  void Execute(string[] args,out ConsoleResponse response)
        {
            string path = string.Join("", args);
            response = new(path);

            PathValidation pathValidation = new(_FilePathValidator, path);
            FileReadable fileReadable = new (_FileReader, path);
            FormatValidation formatValidation = new(_FormatValidator);
            Calculate calculate = new (_ExchangeCalculator);

            pathValidation.SetSuccessor(fileReadable);
            fileReadable.SetSuccessor(formatValidation);
            formatValidation.SetSuccessor(calculate);

            pathValidation.ProcessRequest(response);

        }
    }
}
