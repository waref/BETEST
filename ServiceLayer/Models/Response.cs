
namespace ServiceLayer.Models
{
    public class Response
    {
        public decimal ExchangeResult { get; set; }
        bool IsSucceded { get; set; }=false;
        public string ErrorMessages { get; set; } = string.Empty;
    }
}
