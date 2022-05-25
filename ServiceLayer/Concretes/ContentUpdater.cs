using ServiceLayer.Abstracts;
using ServiceLayer.Models;
using ServiceLayer.Statics;


namespace ServiceLayer.Concretes
{
    public class ContentUpdater : FormatValidatorWorkFlow
    {
        public override void ProcessRequest(FormatValidationToken token)
        {
            if(successor != null)
            {
            token.Data = token.Data.Select(x => x.ToUpper());
            successor.ProcessRequest(token);

            }
            else
            {
                token.IsValidForNextStep = false;
                token.ErrorMessage = Static.WRONG_WHILEUPDATING;
            }
        }
    }
}
