using ServiceLayer.IServices;
using ServiceLayer.Statics;

namespace ServiceLayer.Services
{
    public class FilePathValidator : IFilePathValidator
    {
        public bool IsValidPath(string path, ContextTypes? context)
        {
            return context switch
            {
                ContextTypes.TextFile => IsValidFilePath(path),
                ContextTypes.Other => false,
                _ => false,
            };
        }

        private static bool IsValidFilePath(string path)
        {
            return !String.IsNullOrEmpty(path) && File.Exists(path);
        }


    }
}
