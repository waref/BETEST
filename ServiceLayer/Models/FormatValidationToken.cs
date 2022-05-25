
namespace ServiceLayer.Models
{
    public class FormatValidationToken
    {
        public bool IsValidForNextStep { get; set; }=true;     
        public bool IsValidated { get; set; }=false;
        public string ErrorMessage { get; set; }
        public IEnumerable<string> Data { get; set; }
        public int DataRowscount { get; set; } = 0;
        public FormatValidationToken(IEnumerable<string> data, string errorMessage = "")
        {
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
