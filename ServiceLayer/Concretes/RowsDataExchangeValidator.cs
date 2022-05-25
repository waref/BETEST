using ServiceLayer.Abstracts;
using ServiceLayer.Models;
using ServiceLayer.Statics;

namespace ServiceLayer.Concretes
{
    public class RowsDataExchangeValidator : FormatValidatorWorkFlow
    {
        public override void ProcessRequest(FormatValidationToken token)
        {
            if (CanCheckDataRows(token))
            {
                token.IsValidated = true;
            }
            else
            {
                token.IsValidated = false;
                token.ErrorMessage = Static.WRONG_DATAROWS;
            }
        }

        private static bool CanCheckDataRows(FormatValidationToken token)
        {
            return token.IsValidForNextStep && IsValidDataRows(token);
        }

        private static bool IsValidDataRows(FormatValidationToken token)
        {
            IEnumerable<string> data = token.Data.Skip(2);
         bool isRowsNumberMatchingDataCount= data.Count() == token.DataRowscount;           
            return isRowsNumberMatchingDataCount && data.All(x => StringHelper.IsRegexValid(x,Static.REG_REQUEST_DATALINE));
        }
    }
}
