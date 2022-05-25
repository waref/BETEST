using Engine.Statics;

namespace Engine.Models
{
    public class ExchangeToken
    {
        public bool IsValidForNextStep { get; set; } = true;
        public Currency Start { get; set; }
        public Currency Cibled { get; set; }
        public IEnumerable<string> RowData { get;}
        public ExecutionStates ExecutionState { get; set; }
        public int DataRowscount { get; set; } = 0;
        public string[] Vertices { get; set; }
        public Tuple<string?, string?>[] Edges { get; set; }
        public IEnumerable<string> ShortestPath { get; set; }
        public string ErrorMessage { get; set; }
        public decimal ExchangeResult { get; set; } = 0;
        public ExchangeToken(IEnumerable<string> rowData)
        {
            RowData = rowData ?? new List<string>();
            Start = new();
            Cibled = new();

        }
        public ExchangeToken(IEnumerable<string> rowData, int dataRowscount)
        {
            RowData = rowData ?? new List<string>();
            DataRowscount= dataRowscount;

        }
    }
}
