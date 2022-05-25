namespace Engine.Models
{
    public partial class Graph<T>
    {

        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        public Graph(){}

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }


        public void AddVertex(T vertex)
        {
            if(vertex!= null)
            {
                AdjacencyList[vertex] = new HashSet<T>();
            }
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (edge != null &&AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}
