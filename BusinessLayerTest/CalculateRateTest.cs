using Engine.Concretes;
using Engine.Models;
using Engine.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    public class CalculateRateTest
    {
        [Test]
        public void StoreDiagonalCreatedTrue()
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

            ExchangeToken token = new(data,6);
            token.Start = new Currency("EUR");
            token.Cibled = new Currency("JPY");
            token.Vertices = new string[] { "AUD", "CHF", "JPY", "KRW", "EUR", "USD", "INR" };
            token.ShortestPath= new[] { "EUR", "CHF", "AUD", "JPY" };
            CalculateRate calculator = new(new CurrencyMatrixGenerator());
          
            //WHEN
            calculator.ProcessRequest(token);

            //THEN
            Assert.AreEqual(59033, token.ExchangeResult);
        }
    }
}
