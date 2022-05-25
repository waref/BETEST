
using Engine.Models;
using Engine.Services;
using Engine.Statics;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    public class ExchangeCalculatorTest
    {
        [Test]
        public void CalculateExchangeProcesNullTokenFalse()
        {
            //GIVEN
            ExchangeToken token = null;
            ExchangeCalculatorProcess calculator = new();

            //WHEN
            CalculationResponse response = calculator.GetCalculationsResult(token);

            //THEN
            Assert.AreEqual(false, response.IsSucceded);
            Assert.AreEqual(Static.WRONG_INPUT, response.ErrorMessage);
        }
        [Test]
        public void CalculateExchangeProcesMissingTokenDataRowscountFalse()
        {
            //GIVEN
            List<string> data = new()
            {
                "EUR;550;JPY",
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571",
            };

            ExchangeToken token = new(data);
            ExchangeCalculatorProcess calculator = new();

            //WHEN
            CalculationResponse response = calculator.GetCalculationsResult(token);

            //THEN
            Assert.AreEqual(false, response.IsSucceded);
            Assert.AreEqual(Static.WRONG_TOKENDATA, response.ErrorMessage);
        }

        [Test]
        public void CalculateExchangeProcesTrue()
        {
            //GIVEN
            
            List<string> data = new()
            {
                "EUR;550;JPY",
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571",
            };
            //Data extracted after the read
            ExchangeToken token = new(data);
            token.DataRowscount = 6;
            ExchangeCalculatorProcess calculator = new();

            //WHEN
            CalculationResponse response= calculator.GetCalculationsResult(token);

            //THEN
            Assert.AreEqual(59033, response.Results);
        }
    }
}
