using NUnit.Framework;
using ServiceLayer.Services;
using ServiceLayer.Statics;
using System;

namespace ServicesTests
{
    public class FileValidatorTest
    {
        private readonly FilePathValidator fileValidator=new();

        [Test]
        public void IsValidNullContext()
        {
            //GIVEN
            ContextTypes? context = null;
            string path = @"C:\";
            //WHEN
            bool isValidated = fileValidator.IsValidPath(path, context);
            //THEN
            Assert.AreEqual(false, isValidated);
        }

        [Test]
        public void IsValidOtherContext()
        {
            //GIVEN
            string path = @"C:\";
            //WHEN
            bool isValidated = fileValidator.IsValidPath(path, ContextTypes.Other);
            //THEN
            Assert.AreEqual(false, isValidated);
        }
        
        [Test]
        public void IsValidEmptyPathFileContext()
        {
            //GIVEN
            string path = "";
            //WHEN
            bool isValidated = fileValidator.IsValidPath(path, ContextTypes.TextFile);
            //THEN
            Assert.AreEqual(false, isValidated);
        }

        [Test]
        public void IsValidGoodPathFileContext()
        {
            //GIVEN
            string path = GetProjectFileDirecotry();
            //WHEN
            bool isValidated = fileValidator.IsValidPath(path, ContextTypes.TextFile);
            //THEN
            Assert.AreEqual(true, isValidated);
        }
        
        
         [Test]
        public void IsValidWrongPathFileContext()
        {
            //GIVEN
            string path = @"C:\ABC\DEF\GHJ\ .txt";
            //WHEN
            bool isValidated = fileValidator.IsValidPath(path, ContextTypes.TextFile);
            //THEN
            Assert.AreEqual(false, isValidated);
        }


        private static string GetProjectFileDirecotry()
        {
            return AppDomain.CurrentDomain.BaseDirectory + System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name + Static.DLL;

        }

    }
}