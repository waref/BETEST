using DataLayer.IServices;
using ServiceLayer.Models;

namespace DataLayer.Services
{
    public class FileReader : IFileReader
    {
        public FileResponse ReadText(string filePath)
        {
            try
            {
                return new FileResponse(File.ReadAllLines(filePath), true);

            }
            catch (Exception ex)
            {
                return new FileResponse(Array.Empty<string>(), false, ex.Message);
            }
        }
    }
}
