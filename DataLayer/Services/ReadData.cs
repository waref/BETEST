using DataLayer.IServices;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class ReadData : IFileReader
    {
        public FileResponse ReadText(string filePath)
        {
            try
            {
              return   new FileResponse(File.ReadAllLines(filePath),true);

            }catch(Exception ex)
            {
                return new FileResponse(Array.Empty<string>(), false,ex.Message);
            }
        }
    }
}
