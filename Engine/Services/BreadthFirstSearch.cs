using Engine.IServices;
using Engine.Models;
using Engine.Statics;

namespace Engine.Services
{
    public class BreadthFirstSearch : IBreadthFirstSearch
    {
        private string _ErrorMessage;
        private ExecutionStates _ExecutionState;
        public BreadthFirstSearch()
        {
            _ErrorMessage = string.Empty;
        }
        public ExecutionStates GetExecutionResult(out string message)
        {
            message = _ErrorMessage;
            return _ExecutionState;
        }

        public bool IsValidForCalculation<T>(Graph<T> graph)
        {
            return graph!=null &&  graph.AdjacencyList.Count > 0;
        }

        /// <summary>
        /// Calculate the shortest path for a undirected nad unweighted Graph.
        /// </summary>
        /// <typeparam name="T">Genereic Type</typeparam>
        /// <param name="graph">Object that represent the definition of the type Grap</param>
        /// <param name="start">Starting vertex</param>
        /// <returns>The starting vertex if no road , otherwise the list of vertex of shortest road</returns>
        public Func<T, IEnumerable<T>> ShortestPathFunction<T>(Graph<T> graph, T start)
        {
            if (IsValidForCalculation(graph))
            {
                Dictionary<T, T> previous = new();
                BuildGraphShape(graph, start, previous);
                IEnumerable<T> shortestPath(T vertex)
                {
                    List<T> path = new();
                    T current = vertex;
                    while (current !=null && !current.Equals(start))
                    {
                        if (!previous.ContainsKey(current))
                        {
                            _ErrorMessage = Static.WRONG_NOPATH;
                            _ExecutionState = ExecutionStates.NoPathFoud;
                            break;
                        }
                        path.Add(current);
                        current = previous[current];
                    }
                    path.Add(start);
                    path.Reverse();

                    return path;
                }
                _ExecutionState = _ExecutionState != ExecutionStates.Default ? _ExecutionState : ExecutionStates.OK;
                return shortestPath;
            }
            else
            {
                _ExecutionState = ExecutionStates.KO;
                _ErrorMessage = Static.WRONG_INVALIDGRAPH;
                IEnumerable<T> nullPath(T v)
                {
                    return new List<T>();
                }
                return nullPath;
            }

        }

        private static void BuildGraphShape<T>(Graph<T> graph, T start, Dictionary<T, T> previous)
        {
            Queue<T> queue = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}
