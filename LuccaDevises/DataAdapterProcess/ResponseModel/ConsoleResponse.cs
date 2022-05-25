namespace LuccaDevises.DataAdapterProcess.ResponseModel
{
    public class ConsoleResponse
    {
        public bool IsValidForNext { get; set; } = true;
        public string ConsoleMessage { get; set; }
        public decimal Exchangeresult { get; set; }
        public IEnumerable<string> Data { get; set; }
        public int DataRowscount { get; set; } = 0;
        public string Path { get; set; } = "";
        public bool IsSuceeded { get; set; }
        public ConsoleResponse(string path) { Path = path; }
        public ConsoleResponse( decimal exchangeresult,string consoleMessage="")
        {
            ConsoleMessage = consoleMessage;
            Exchangeresult = exchangeresult;
        }
    }
}
