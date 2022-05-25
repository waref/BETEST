using Engine.Models;
using System.Data;

namespace Engine.IServices
{
    public interface ICurrencyMatrixGenerator
    {     
        void InitCurrencyResume(List<Currency> currencies);
        void Store(Currency from, Currency to, decimal exchangeRate);
        DataTable GetMatrix();
    }
}
