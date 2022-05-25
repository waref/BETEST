using Engine.Abstracts;
using Engine.IServices;
using Engine.Models;
using Engine.Statics;

namespace Engine.Concretes
{
    public class ExchangeRatesDataAdapter : ExchangeCalculationFlow
    {
        private readonly IBreadthFirstSearch _IBreadthFirstSearch;       
        public ExchangeRatesDataAdapter(IBreadthFirstSearch breadthFirstSearch)
        {
            _IBreadthFirstSearch = breadthFirstSearch;
            Graph = new Graph<string?>();
        }
        public Graph<string?> Graph { get; set; }
        public override void ProcessRequest(ExchangeToken token)
        {
            if (successor != null && IsValidTokenForCalulation(token))
            {
                UpdateTokenData(token);
                Graph = new(token.Vertices, token.Edges);
                if ( _IBreadthFirstSearch.IsValidForCalculation(Graph) && IsValidVertices(token))
                {
                    token.IsValidForNextStep = true;
                    string? startVertex = token.Vertices.FirstOrDefault(x => x == token.Start.Name);
                    string? endVertex = token.Vertices.FirstOrDefault(x => x == token.Cibled.Name);
                    Func<string?, IEnumerable<string?>> shortestPathTo = _IBreadthFirstSearch.ShortestPathFunction(Graph, startVertex);
                    token.ShortestPath = shortestPathTo(endVertex);

                    token.IsValidForNextStep = true;
                    token.ExecutionState = ExecutionStates.OK;
                    successor.ProcessRequest(token);
                }
                else
                {
                    token.IsValidForNextStep = false;
                    token.ExecutionState = ExecutionStates.KO;
                    token.ErrorMessage = Static.WRONG_ERRORINCALCULATION;
                }

            }
            else
            {
                token.IsValidForNextStep = false;
                token.ExecutionState = ExecutionStates.KO;
                token.ErrorMessage = Static.WRONG_TOKENDATA;
            }

        }
       private static bool IsValidTokenForCalulation(ExchangeToken token)
        {
            return token.IsValidForNextStep
                && token.DataRowscount != 0
                && !String.IsNullOrEmpty(token.Start.Name)
                && !String.IsNullOrEmpty(token.Cibled.Name);
        }
        private static void UpdateTokenData(ExchangeToken token)
        {

            Tuple<string?, string?>[] edges = new Tuple<string?, string?>[token.DataRowscount];
            List<string> vertices = new List<string>();
            List<string> data = token.RowData.Skip(2).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                string line = data.ElementAt(i);
                string[] splittedRequest = Static.FormatRequest(line);
                string? currency1 = new(splittedRequest[0]);
                string? currency2 = new(splittedRequest[1]);
                if (!vertices.Any(x => x == currency1))
                    vertices.Add(currency1);
                if (!vertices.Any(x => x == currency2))
                    vertices.Add(currency2);
                edges[i] = Tuple.Create(currency1, currency2);
            }
            token.Edges = edges;
            token.Vertices = vertices.ToArray();
            
        }
        private static bool IsValidVertices(ExchangeToken token)
        {
            return token.Vertices != Array.Empty<string>() && token.Vertices.Count(x => x == token.Start.Name) == 1 && token.Vertices.Count(x => x == token.Cibled.Name) == 1;
        }
       
    }
}
