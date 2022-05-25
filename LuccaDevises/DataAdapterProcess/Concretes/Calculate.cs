using Engine.IServices;
using Engine.Models;
using LuccaDevises.DataAdapterProcess.Abstract;
using LuccaDevises.DataAdapterProcess.ResponseModel;

namespace LuccaDevises.DataAdapterProcess.Concretes
{
    public class Calculate : ExecutionFlow
    {
        private readonly IExchangeCalculator<ExchangeToken> _ExchangeCalculator;

        public Calculate(IExchangeCalculator<ExchangeToken> exchangeCalculator)
        {
            _ExchangeCalculator = exchangeCalculator;
        }

        public override void ProcessRequest(ConsoleResponse token)
        {
          if( token.IsValidForNext)
            {
                ExchangeToken Calculationtoken = new ExchangeToken(token.Data, token.DataRowscount);
                CalculationResponse calculationResponse = _ExchangeCalculator.GetCalculationsResult(Calculationtoken);
                if (calculationResponse.IsSucceded)
                {
                    token.Exchangeresult = calculationResponse.Results;
                    token.ConsoleMessage = " Calculation done!";
                    token.IsSuceeded = true;
                }
                else
                {
                    token.ConsoleMessage = calculationResponse.ErrorMessage;
                }
            }
        }
    }
}
