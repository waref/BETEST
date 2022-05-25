using LuccaDevises.DataAdapterProcess.ResponseModel;

namespace LuccaDevises.DataAdapterProcess.Abstract
{
    public abstract class ExecutionFlow
    {
        protected ExecutionFlow? successor;
        public void SetSuccessor(ExecutionFlow workFlow)
        {
            successor = workFlow;
        }
        public abstract void ProcessRequest(ConsoleResponse token);
    }
}
