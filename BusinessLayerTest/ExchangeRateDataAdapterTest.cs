using Engine.Concretes;
using Engine.IServices;
using Engine.Models;
using Engine.Services;
using Engine.Statics;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayerTest
{
    public class ExchangeRateDataAdapterTest
    {
        [Test]
        public void BuildGraphFalseTokenTestFalse()
        {
            //GIVEN
            List<string> data = new()
            {

                "EUR;550;JPY",
                "6",
            };

            ExchangeToken token = new(data);
            token.IsValidForNextStep = false;
            token.Start = new Currency("EUR");
            token.Cibled = new Currency("JPY");
            token.Vertices = new string[0];
            token.DataRowscount = 6;
            ExchangeRatesDataAdapter dataAdapter = new(new BreadthFirstSearch());
            dataAdapter.SetSuccessor(new ExchangeRatesDataAdapter(new BreadthFirstSearch()));
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO, token.ExecutionState);
            Assert.AreEqual(Static.WRONG_TOKENDATA, token.ErrorMessage);
        }
        [Test]
        public void BuildGraphNoSuccesorTestFalse()
        {
            //GIVEN
            List<string> data = new()
            {

                "EUR;550;JPY",
                "6",
            };

            ExchangeToken token = new(data);
            token.IsValidForNextStep = false;
            token.Start = new Currency("EUR");
            token.Cibled = new Currency("JPY");
            token.Vertices = new string[0];
            token.DataRowscount = 6;
            ExchangeRatesDataAdapter dataAdapter = new(new BreadthFirstSearch());
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO, token.ExecutionState);
            Assert.AreEqual(Static.WRONG_TOKENDATA, token.ErrorMessage);
        }
        [Test]
        public void BuildGraphNoStartNoEndFalseTest()
        {
            //GIVEN
            List<string> data = new()
            {

                "EUR;550;JPY",
                "6",
            };

            ExchangeToken token = new(data);
            token.IsValidForNextStep = true;
            token.Vertices = new string[0];
            token.DataRowscount = 6;
            ExchangeRatesDataAdapter dataAdapter = new(new BreadthFirstSearch());
            dataAdapter.SetSuccessor(new ExchangeRatesDataAdapter(new BreadthFirstSearch()));
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO, token.ExecutionState);
            Assert.AreEqual(Static.WRONG_TOKENDATA, token.ErrorMessage);
        }
        [Test]
        public void ProcessRequestFindPathBreadthFirstSearchFalseTest()
        {
            //GIVEN
            Mock<IBreadthFirstSearch> mock = new Mock<IBreadthFirstSearch>();
            mock.Setup(x=>x.IsValidForCalculation(It.IsAny<Graph<string?>>())).Returns(false);
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
            token.IsValidForNextStep = true;
            token.Start = new Currency("EUR");
            token.Cibled = new Currency("JPY");
            token.DataRowscount = 6;
            ExchangeRatesDataAdapter dataAdapter = new(mock.Object);
            dataAdapter.SetSuccessor(new ExchangeRatesDataAdapter(new BreadthFirstSearch()));
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO, token.ExecutionState);
            Assert.AreEqual(Static.WRONG_ERRORINCALCULATION, token.ErrorMessage);
        }
        [Test]
        public void ProcessRequestFindPathTrueTest()
        {
            //GIVEN
            IEnumerable<string?> expectedShortRoad = new[] { "EUR", "CHF", "AUD", "JPY" };

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
            token.IsValidForNextStep = true;
            token.Start = new Currency("EUR");
            token.Cibled = new Currency("JPY");
            token.DataRowscount = 6;
            ExchangeRatesDataAdapter dataAdapter = new(new BreadthFirstSearch());
            dataAdapter.SetSuccessor(new ExchangeRatesDataAdapter(new BreadthFirstSearch()));
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.IsTrue(Enumerable.SequenceEqual(expectedShortRoad, token.ShortestPath));
        }
    }
}
