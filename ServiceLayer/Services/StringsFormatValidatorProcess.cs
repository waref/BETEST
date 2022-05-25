using ServiceLayer.Concretes;
using ServiceLayer.IServices;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class StringsFormatValidatorProcess : IFormatValidator<IEnumerable<string>>
    {
        public FormatValidationToken? FormatValidationToken { get; set; }

        public FormatValidationToken? GetFormatValidationToken()
        {
            return FormatValidationToken;
        }

        public bool IsValid(IEnumerable<string> data)
        {
            FormatValidationToken = new(data);
            // Setup Validation Chain of Responsibility 

            LengthValidation lengthValidation = new();
            ContentUpdater contentUpdater = new();
            RequestValidation requestValidation = new();
            RowsCountValidaotr rowsCountValidaotr = new();
            RowsDataExchangeValidator rowsDataExchangeValidator = new();

            lengthValidation.SetSuccessor(contentUpdater);
            contentUpdater.SetSuccessor(requestValidation);
            requestValidation.SetSuccessor(rowsCountValidaotr);
            rowsCountValidaotr.SetSuccessor(rowsDataExchangeValidator);

            lengthValidation.ProcessRequest(FormatValidationToken);

            return FormatValidationToken.IsValidated;

        }

       
    }
}
