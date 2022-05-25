
namespace Engine.Models
{
    public class Currency
    {
        public string Name { get; }
        public decimal Amount { get; set; }
        public Currency()
        {
            Name = string.Empty;
            Amount = 0;
        }
        public Currency(string name)
        {
            Name = name;
            Amount = 0;
        }
        public Currency(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
