namespace Engine.Models
{
    public class CalculationResponse
    {
        public decimal Results { get; set; }
        public bool IsSucceded { get; set; }
        public string ErrorMessage { get; set; }
        public CalculationResponse(decimal results, bool isSucceded, string errorMessage="")
        {
            Results = results;
            IsSucceded = isSucceded;
            ErrorMessage = errorMessage;
        }
    }
}
