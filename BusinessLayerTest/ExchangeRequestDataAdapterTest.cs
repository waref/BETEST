using Engine.Concretes;
using Engine.Models;
using Engine.Statics;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    public class ExchangeRequestDataAdapterTest
    {
        [Test]
        public void FormatEmptyDataFalse()
        {
            //GIVEN
            ExchangeToken  token= new(new List<string>());
            ExchangeRequestDataAdapter dataAdapter = new();
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(string.Empty,token.Start.Name);
            Assert.AreEqual(string.Empty,token.Cibled.Name);
        }
        [Test]
        public void FormatDataWrongSeparatorFalse()
        {
            //GIVEN
            List<string> rowLine = new() { "EUR#550#JPY" };
            ExchangeToken token = new(rowLine);
            ExchangeRequestDataAdapter dataAdapter = new();
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO,token.ExecutionState);

        }
        [Test]
        public void FormatDataNoSuccessorFalse()
        {
            //GIVEN
            List<string> rowLine = new() { "EUR;550;JPY" };
            ExchangeToken token = new(rowLine);
            ExchangeRequestDataAdapter dataAdapter = new();
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual(ExecutionStates.KO, token.ExecutionState);

        }
        [Test]
        public void FormatDataTrue()
        {
            //GIVEN
            List<string> rowLine = new (){ "EUR;550;JPY" };
            ExchangeToken token = new (rowLine);
            ExchangeRequestDataAdapter dataAdapter = new();
            dataAdapter.SetSuccessor(new ExchangeRequestDataAdapter());
            //WHEN
            dataAdapter.ProcessRequest(token);

            //THEN
            Assert.AreEqual("EUR", token.Start.Name);
            Assert.AreEqual(550M, token.Start.Amount);
            Assert.AreEqual("JPY", token.Cibled.Name);
        }
    }
}
