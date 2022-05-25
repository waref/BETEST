using DataLayer.Models;

namespace DataLayer.IServices
{
    public interface IFileReader
    {
        FileResponse ReadText(string filePath);
    }
}
