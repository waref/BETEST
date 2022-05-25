using ServiceLayer.Statics;

namespace ServiceLayer.IServices
{
    /// <summary>
    /// Path validity Service regarding context
    /// </summary>
    public interface IFilePathValidator
    {
        /// <summary>
        /// Check path validity
        /// </summary>
        /// <param name="path"> String : path value</param>
        /// <param name="context"> ContextType : type of context ( File, url, ..)</param>
        /// <returns>True if path exists </returns>
        bool IsValidPath(string path, ContextTypes? context);

    }
}
