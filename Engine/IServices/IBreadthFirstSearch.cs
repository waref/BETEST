using Engine.Models;
using Engine.Statics;

namespace Engine.IServices
{
    public interface IBreadthFirstSearch
    {
        bool IsValidForCalculation<T>(Graph<T> graph);
        ExecutionStates  GetExecutionResult(out string message);
        Func<T, IEnumerable<T>> ShortestPathFunction<T>(Graph<T> graph, T start);
    }
}
