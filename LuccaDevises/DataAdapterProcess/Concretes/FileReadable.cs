using DataLayer.IServices;
using LuccaDevises.DataAdapterProcess.Abstract;
using LuccaDevises.DataAdapterProcess.ResponseModel;
using ServiceLayer.Models;

namespace LuccaDevises.DataAdapterProcess.Concretes
{
    public class FileReadable : ExecutionFlow
    {
        private readonly IFileReader _FileReader;
        private readonly string _Path;
        public FileReadable(IFileReader fileReader, string path)
        {
            _FileReader = fileReader;
            _Path = path;
        }

        public override void ProcessRequest(ConsoleResponse token)
        {
           if(successor!=null && token.IsValidForNext)
            {
                FileResponse IsValidForRead = _FileReader.ReadText(_Path);
                if (IsValidForRead.IsSucceded)
                {
                    token.Data = IsValidForRead.ExtractedText;
                    successor.ProcessRequest(token);
                }
                else
                {
                    token.IsValidForNext= false;
                    token.ConsoleMessage=  IsValidForRead.ErrorMessage;
                }
            }
        }
    }
}
