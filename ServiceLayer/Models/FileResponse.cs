namespace ServiceLayer.Models
{
    public class FileResponse
    {
        public string ErrorMessage { get; }
        public IEnumerable<string> ExtractedText { get; }
        public bool IsSucceded { get; }
        public FileResponse(IEnumerable<string> extractedText, bool isSucceded, string errorMessage = "")
        {
            ErrorMessage = errorMessage;
            ExtractedText = extractedText;
            IsSucceded = isSucceded;
        }
    }
}
