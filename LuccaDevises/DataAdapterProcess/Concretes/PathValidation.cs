using LuccaDevises.DataAdapterProcess.Abstract;
using LuccaDevises.DataAdapterProcess.ResponseModel;
using ServiceLayer.IServices;

namespace LuccaDevises.DataAdapterProcess.Concretes
{
    public class PathValidation : ExecutionFlow
    {
        private readonly IFilePathValidator _FilePathValidator;
        private readonly string _Path;
        public PathValidation(IFilePathValidator filePathValidator, string path)
        {
            _FilePathValidator = filePathValidator;
            _Path = path;
        }

        public override void ProcessRequest(ConsoleResponse token)
        {
           if(successor!= null)
            {
                bool isValidPath = _FilePathValidator.IsValidPath(_Path, ServiceLayer.Statics.ContextTypes.TextFile);
                if (isValidPath)
                {
                    successor.ProcessRequest(token);
                }
                else
                {
                    token.IsValidForNext = false;
                    token.ConsoleMessage = "Wrong File Path";
                }
                   

            }
        }
    }
}
