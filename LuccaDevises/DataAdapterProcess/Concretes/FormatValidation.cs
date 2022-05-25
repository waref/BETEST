using LuccaDevises.DataAdapterProcess.Abstract;
using LuccaDevises.DataAdapterProcess.ResponseModel;
using ServiceLayer.IServices;
using ServiceLayer.Models;

namespace LuccaDevises.DataAdapterProcess.Concretes
{
    public class FormatValidation : ExecutionFlow
    {
        private readonly IFormatValidator<IEnumerable<string>> _FormatValidator;

        public FormatValidation(IFormatValidator<IEnumerable<string>> formatValidator)
        {
            _FormatValidator = formatValidator;
        }

        public override void ProcessRequest(ConsoleResponse token)
        {
            if(successor!=null && token.IsValidForNext && _FormatValidator.IsValid(token.Data))
            {
                FormatValidationToken formatValidatorToken = _FormatValidator.GetFormatValidationToken();
                token.Data= formatValidatorToken.Data;
                token.DataRowscount=formatValidatorToken.DataRowscount;
                successor.ProcessRequest(token);
            }
            else
            {
                token.ConsoleMessage = "Invalid Data Format"; 
            }
        }
    }
}
