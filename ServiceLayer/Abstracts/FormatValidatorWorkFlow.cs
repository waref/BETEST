using ServiceLayer.Models;

namespace ServiceLayer.Abstracts
{
    public abstract class FormatValidatorWorkFlow
    {
        protected FormatValidatorWorkFlow? successor;
        public void SetSuccessor(FormatValidatorWorkFlow workFlow)
        {
            successor = workFlow;
        }
        public abstract void ProcessRequest(FormatValidationToken token);
    }
}
