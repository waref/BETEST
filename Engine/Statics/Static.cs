
namespace Engine.Statics
{
    public static class Static
    {
        public static readonly string WRONG_NOPATH= "No Path found !";
        public static readonly string WRONG_EXCEPTION= "Exception found while computing the shortest path !";
        public static readonly string WRONG_INVALIDGRAPH ="Invalid Graph !";
        public static readonly string CURRENCYNAMEFROMTO = "CurrencyNameFromTO";
        public static readonly char SEPARATOR = ';';
        public static readonly string WRONG_TOKENDATA = "Wrong input Token Data !";
        public static readonly string WRONG_ERRORINCALCULATION = "Error during calculation  !";
        public static readonly string WRONG_INPUT = "Wrong input for caluculation service !";
        public static string[] FormatRequest(string input)
        {
            string[] splittedRequest = input.Split(Static.SEPARATOR);
            return splittedRequest;
        }
    }
}
