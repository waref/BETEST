using Engine.Models;

namespace Engine.Abstracts
{
    public abstract class ExchangeCalculationFlow
    {
        protected ExchangeCalculationFlow? successor;
        public void SetSuccessor(ExchangeCalculationFlow workFlow)
        {
            successor = workFlow;
        }
        public abstract void ProcessRequest(ExchangeToken token);
    }
}
