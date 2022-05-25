using Engine.Abstracts;
using Engine.IServices;
using Engine.Models;
using Engine.Statics;
using System.Data;
using System.Globalization;

namespace Engine.Concretes
{
    public class CalculateRate : ExchangeCalculationFlow
    {
        private readonly ICurrencyMatrixGenerator _CurrencyMatrixGenerator;
       public CalculateRate(ICurrencyMatrixGenerator currencyMatrixGenerator)
        {
            _CurrencyMatrixGenerator = currencyMatrixGenerator;
        }
        public override void ProcessRequest(ExchangeToken token)
        {
            try
            {
                if (token.IsValidForNextStep)
                {
                    List<Currency> curencies = GetTokenCurrencies(token);
                    UpdateAmount(token);
                    _CurrencyMatrixGenerator.InitCurrencyResume(curencies);
                    BulkStore(_CurrencyMatrixGenerator, token);
                    token.ExchangeResult = GetCalulationResult(token);
                    token.ExecutionState = ExecutionStates.OK;
                }               
            }catch(Exception ex)
            {
                token.ExecutionState = ExecutionStates.KO;
                token.ErrorMessage = ex.Message;
            }
           
        }

        private decimal GetCalulationResult( ExchangeToken token)
        {
            DataTable resume = _CurrencyMatrixGenerator.GetMatrix();
            decimal result = token.Start.Amount;
            for (int i = 0; i < token.ShortestPath.Count()-1; i++)
            {
                int fromIndex = resume.Columns.IndexOf(token.ShortestPath.ElementAt(i));
                int toIndex = resume.Columns.IndexOf(token.ShortestPath.ElementAt(i+1));
                result *= (Decimal)resume.Rows[fromIndex - 1][toIndex]; 
            }
            return Decimal.Round(result, 0, MidpointRounding.AwayFromZero);
        }

        private static void BulkStore(ICurrencyMatrixGenerator currencyMatrixGenerator, ExchangeToken token)
        {
            List<string> data = token.RowData.Skip(2).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                string line = data.ElementAt(i);
                string[] splittedRequest = Static.FormatRequest(line);
                Currency currencyFrom = new(splittedRequest[0]);
                Currency currencyTo = new(splittedRequest[1]);
                decimal exchange = decimal.Parse(splittedRequest[2], CultureInfo.InvariantCulture);
                currencyMatrixGenerator.Store(currencyFrom, currencyTo, exchange);
            }
        }

        private static void UpdateAmount(ExchangeToken token)
        {
            string resquest = token.RowData.First();
            string amount = Static.FormatRequest(resquest)[1];
            token.Start.Amount = decimal.Parse(amount);
        }

        private static List<Currency> GetTokenCurrencies(ExchangeToken token)
        {
            List<Currency> currencies = new List<Currency>();
            foreach (string item in token.Vertices)
            {
                currencies.Add(new Currency(item));
            }
            return currencies;
        }

    }
}
