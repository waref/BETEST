using Engine.IServices;
using Engine.Models;
using Engine.Statics;
using System.Data;

namespace Engine.Services
{
    public class CurrencyMatrixGenerator : ICurrencyMatrixGenerator
    {
        private DataTable _CurrencyResume;
        private List<string>? _FiltredNames;

        public CurrencyMatrixGenerator()
        {
            _CurrencyResume = new DataTable();
        }

        public void InitCurrencyResume(List<Currency> currencies)
        {
            _CurrencyResume = Init(currencies);
        }

        public void Store(Currency from, Currency to, decimal exchangeRate)
        {
            if (IsValidForStorage(exchangeRate))
            {
                _CurrencyResume.Rows[_FiltredNames.IndexOf(from.Name)][_FiltredNames.IndexOf(to.Name) + 1]
                    = Decimal.Round(exchangeRate, 4, MidpointRounding.AwayFromZero);
                _CurrencyResume.Rows[_FiltredNames.IndexOf(to.Name)][_FiltredNames.IndexOf(from.Name) + 1]
                    = Decimal.Round(1 / exchangeRate, 4, MidpointRounding.AwayFromZero);
            }

        }
        public DataTable GetMatrix()
        {
            return _CurrencyResume;
        }
        private DataTable Init(List<Currency> currencies)
        {
            DataTable currencResume = new();
            if (currencies != null && currencies.Any())
            {
                _FiltredNames = currencies.GroupBy(x => x.Name).Select(x => x.First().Name).ToList();
                DataColumn[] _Columns = new DataColumn[_FiltredNames.Count + 1];
                _Columns[0] = new DataColumn(Static.CURRENCYNAMEFROMTO, typeof(string));
                currencResume.Columns.Add(_Columns[0]);
                for (int i = 1; i <= _FiltredNames.Count; i++)
                {
                    DataColumn column = new (_FiltredNames[i - 1], typeof(decimal));
                    currencResume.Columns.Add(column);
                    _Columns[i] = column;
                    DataRow row = currencResume.NewRow();
                    row[Static.CURRENCYNAMEFROMTO] = _FiltredNames[i - 1];


                    currencResume.Rows.Add(row);
                    currencResume.Rows[i - 1][i] = 1;
                }
            }
            return currencResume;
        }

        private bool IsValidForStorage(decimal exchangeRate)
        {
            return exchangeRate != 0 && _FiltredNames != null && _FiltredNames.Any() && _CurrencyResume != null && _CurrencyResume.Rows.Count > 0;
        }
    }
}
