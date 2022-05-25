using ServiceLayer.Abstracts;
using ServiceLayer.Models;
using ServiceLayer.Statics;

namespace ServiceLayer.Concretes
{
    internal class LengthValidation : FormatValidatorWorkFlow
    {
        public override void ProcessRequest(FormatValidationToken token)
        {
            if (IsLenghValid(token.Data) && successor != null)
            {
                token.IsValidForNextStep = true;
                successor.ProcessRequest(token);
            }
            else
            {
                token.IsValidForNextStep = false;
                token.ErrorMessage =Static.WRONG_LENGHT;
            }
        }

        private static bool IsLenghValid(IEnumerable<string> data)
        {
            return data != null && data.Count() > Static.MINROWSCOUNT;
        }
    }
}
