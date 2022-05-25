using ServiceLayer.Models;

namespace ServiceLayer.IServices
{
    /// <summary>
    /// Format Validation Service process
    /// </summary>
    /// <typeparam name="T"> input generic object </typeparam>
    public interface IFormatValidator<in T>
    {
        /// <summary>
        /// Check is the data have a valid format
        /// </summary>
        /// <param name="data"> Generic Type</param>
        /// <returns>True if the input have a valid format of data</returns>
        bool IsValid(T data);
        FormatValidationToken? GetFormatValidationToken();
    }
}
