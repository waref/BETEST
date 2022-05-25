using ServiceLayer.Models;

namespace DataLayer.IServices
{
    /// <summary>
    /// File DAL Service
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Returns test read FileResponse model.
        /// </summary>
        /// <param name="filePath"> string file path</param>
        /// <returns>FileResponse model</returns>
        FileResponse ReadText(string filePath);
    }
}
