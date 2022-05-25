using ServiceLayer.Abstracts;
using ServiceLayer.Models;
using ServiceLayer.Statics;

namespace ServiceLayer.Concretes
{
    internal class RequestValidation : FormatValidatorWorkFlow
    {
        public override void ProcessRequest(FormatValidationToken token)
        {
            if (successor != null && CanCheckRequest(token))
            {
                token.IsValidForNextStep = true;
                successor.ProcessRequest(token);
            }
            else
            {
                token.IsValidForNextStep = false;
                token.ErrorMessage = Static.WRONG_REQUEST_FORMAT;
            }
        }

        private static bool CanCheckRequest(FormatValidationToken token)
        {
            return  token.IsValidForNextStep && IsRequestValid(token.Data.First());
        }

        private static bool IsRequestValid(string data)
        {
            return StringHelper.IsRegexValid(data,Static.REG_REQUEST_FORMAT);
        }
    }
}
