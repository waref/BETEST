using Engine.Abstracts;
using Engine.Models;
using Engine.Statics;

namespace Engine.Concretes
{
    public class ExchangeRequestDataAdapter : ExchangeCalculationFlow
    {
        private string[] _SplittedRequest;
        public ExchangeRequestDataAdapter()
        {
            _SplittedRequest = new string[3];
        }
        public override void ProcessRequest(ExchangeToken token)
        {
            if (successor != null && token.RowData.Any()&& token.RowData.First().Count(x=>x==Static.SEPARATOR)==2)
            {
                FormatRequest(token);
                token.Start = GetStartingCurrency();
                token.Cibled = GetCibledCurrency();
                token.ExecutionState = ExecutionStates.OK;
                token.IsValidForNextStep = true;
                successor.ProcessRequest(token);
            }
            else
            {
                token.ExecutionState = ExecutionStates.KO;
                token.IsValidForNextStep = false;
            }

        }

        private Currency GetCibledCurrency()
        {
            return new Currency(_SplittedRequest[2]);
        }

        private Currency GetStartingCurrency()
        {
            return new Currency(_SplittedRequest[0], decimal.Parse(_SplittedRequest[1]));
        }
        private void FormatRequest(ExchangeToken token)
        {
            _SplittedRequest = token.RowData.First().Split(Static.SEPARATOR);
        }
    }
}
