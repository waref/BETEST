using DataLayer.IServices;
using Moq;
using NUnit.Framework;
using ServiceLayer.Concretes;
using ServiceLayer.Models;
using ServiceLayer.Services;
using ServiceLayer.Statics;
using System.Collections.Generic;

namespace ServicesTests
{
    public class StringsFormatValidatorTest
    {
        private Mock<IFileReader> FileReader;
        private readonly List<string> ValidData = new()
        {
            "EUR;550;JPY",
            "3",
            "EUR;THX;1.2053",
            "BTC;ETH;12.1234",
            "AVA;ADA;30.0001"
        };

        [SetUp]
        public void SetUp()
        {
            FileReader = new Mock<IFileReader>();
            FileReader.Setup(x => x.ReadText(It.IsAny<string>())).Returns(new FileResponse(ValidData, true));
        }


        [Test]
        public void IsValidStringFormatEmptyRequest()
        {
            //GIVEN

            List<string> data = new();
            StringsFormatValidatorProcess formatValidator = new();

            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.AreEqual(false, isValidated);
            Assert.AreEqual(Static.WRONG_LENGHT, formatValidator.FormatValidationToken.ErrorMessage);
        }

        [Test]
        public void IsValidStringFormatWrongLENGHT()
        {
            //GIVEN
            List<string> data = new()
            {
                "HELLO WORLD !",
            };
            StringsFormatValidatorProcess formatValidator = new();

            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.AreEqual(false, isValidated);
            Assert.AreEqual(Static.WRONG_LENGHT, formatValidator.FormatValidationToken.ErrorMessage);
        }

        [Test]
        public void IsValidStringFormatWrongRequest()
        {
            //GIVEN
            List<string> data = new()
            {
                "EUR;JPY;110",
                "3",
                "EUR;THX;1.2053",
                "BTC;ETH;12.1234",
                "AVA;ADA;30.0001"
            };
            StringsFormatValidatorProcess formatValidator = new();

            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.AreEqual(false, isValidated);
            Assert.AreEqual(Static.WRONG_REQUEST_FORMAT, formatValidator.FormatValidationToken.ErrorMessage);
        }

        [Test]
        public void IsValidStringFormatNoCount()
        {
            //GIVEN
            List<string> data = new()
            {
                "EUR;550;JPY",
                "EUR;THX;1.2053",
                "BTC;ETH;12.1234",
                "AVA;ADA;30.0001"
            };
            StringsFormatValidatorProcess formatValidator = new();

            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.AreEqual(false, isValidated);
            Assert.AreEqual(Static.WRONG_ROW_NUMBER, formatValidator.FormatValidationToken.ErrorMessage);
        }


        [Test]
        public void IsValidStringFormatWrongCount()
        {
            //GIVEN
            List<string> data = new()
            {
                "EUR;550;JPY",
                "5",
                "EUR;THX;1.2053",
                "BTC;ETH;12.1234",
                "AVA;ADA;30.0001"
            };
            StringsFormatValidatorProcess formatValidator = new();

            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.AreEqual(false, isValidated);
            Assert.AreEqual(Static.WRONG_DATAROWS, formatValidator.FormatValidationToken.ErrorMessage);
        }

        [Test]
        public void IsValidLowerCaseStringFormatTrue()
        {
            //GIVEN
            List<string> data = new()
            {
                "eur;550;jpy",
                "3",
                "eur;thx;1.2053",
                "btc;eth;12.1234",
                "ava;ada;30.0001"
            };

            StringsFormatValidatorProcess formatValidator = new();
            //WHEN
            bool isValidated = formatValidator.IsValid(data);

            //THEN
            Assert.IsTrue(isValidated);
            Assert.AreEqual(ValidData, formatValidator.FormatValidationToken.Data);
        }

        [Test]
        public void IsValidStringFormatTrue()
        {
            //GIVEN

            StringsFormatValidatorProcess formatValidator = new();
            //WHEN
            bool isValidated = formatValidator.IsValid(ValidData);

            //THEN
            Assert.IsTrue(isValidated);
        }

        [Test]
        public void IsValidFileReaderFormatTrue()
        {
            //GIVEN
            FileResponse response = FileReader.Object.ReadText(" ");
            StringsFormatValidatorProcess formatValidator = new();
            //WHEN
            bool isValidated = formatValidator.IsValid(response.ExtractedText);

            //THEN
            Assert.IsTrue(isValidated);
            Assert.AreEqual(ValidData, formatValidator.FormatValidationToken.Data);
        }

        [Test]
        public void IsValidNoSuccessorContentUpdater()
        {
            //GIVEN
            List<string> data = new();
            StringsFormatValidatorProcess formatValidator = new();
            formatValidator.IsValid(data);
            ContentUpdater contentUpdater = new();

            //WHEN
            contentUpdater.ProcessRequest(formatValidator.FormatValidationToken);
            Assert.IsFalse(formatValidator.FormatValidationToken.IsValidForNextStep);
            Assert.AreEqual(Static.WRONG_WHILEUPDATING, formatValidator.FormatValidationToken.ErrorMessage);
        }

    }
}
