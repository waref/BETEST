using Engine.Concretes;
using Engine.IServices;
using Engine.Models;
using Engine.Statics;

namespace Engine.Services
{
    public class ExchangeCalculatorProcess : IExchangeCalculator<ExchangeToken> 
    {

        public CalculationResponse GetCalculationsResult(ExchangeToken token)
        {
            if (token != null && token.RowData != null)
            {
              
            // Setup Calculation Chain of Responsibility 
            ExchangeRequestDataAdapter exchangeRequestDataAdapter = new();
            ExchangeRatesDataAdapter exchangeRatesDataAdapter = new(new BreadthFirstSearch());
            CalculateRate calculateRate = new(new CurrencyMatrixGenerator());

            exchangeRequestDataAdapter.SetSuccessor(exchangeRatesDataAdapter);
            exchangeRatesDataAdapter.SetSuccessor(calculateRate);
  
            exchangeRequestDataAdapter.ProcessRequest(token);
            return  new CalculationResponse(token.ExchangeResult, token.ExecutionState == ExecutionStates.OK, token.ErrorMessage);
           
            }
            else
            {
                return new CalculationResponse(0, false, Static.WRONG_INPUT);
            }
           

        }
    }
}
