using ServiceLayer.Abstracts;
using ServiceLayer.Models;
using ServiceLayer.Statics;

namespace ServiceLayer.Concretes
{
    public class RowsCountValidaotr : FormatValidatorWorkFlow
    {
        public override void ProcessRequest(FormatValidationToken token)
        {
            if (successor != null && CanCheckRowsCount(token))
            {
                token.IsValidForNextStep = true;
                successor.ProcessRequest(token);
            }
            else
            {
                token.IsValidForNextStep = false;
                token.ErrorMessage = Static.WRONG_ROW_NUMBER;
            }
        }

        private static bool CanCheckRowsCount(FormatValidationToken token)
        {
            string rowsString = token.Data.Skip(1).First();
            if (!string.IsNullOrEmpty(rowsString) && int.TryParse(rowsString, out int rowsCount))
            {
                token.DataRowscount = rowsCount;
                return token.IsValidForNextStep && IsRowCountValid(rowsString);
            }
            return false;
        }

        private static bool IsRowCountValid(string data)
        {
            return StringHelper.IsRegexValid(data, Static.REG_REQUEST_COUNT);
        }
    }
}
