using Engine.Models;
using Engine.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BusinessLayerTest
{
    public class CurrencyMatrixGeneratorTest
    {
     private readonly   CurrencyMatrixGenerator currencyMatrix = new();

        [Test]
        public void StoreWithEmptyCurrenciesFalse()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
    
            currencyMatrix.InitCurrencyResume(new List<Currency> ());

            //WHEN
            currencyMatrix.Store(currency1, currency2, 4);
            int sotredDataCount = currencyMatrix.GetMatrix().Rows.Count;

            //THEN
            Assert.AreEqual(0, sotredDataCount);
        }

        [Test]
        public void StoreDiagonalCreatedTrue()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            Currency currency3 = new("BTC");
            Currency currency4 = new("DOT");
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2, currency3, currency4 });

            //WHEN
            currencyMatrix.Store(currency2, currency4, 4);
            Decimal diagonalSample = (Decimal)currencyMatrix.GetMatrix().Rows[1][2];

            //THEN
            Assert.AreEqual(1, diagonalSample);
        }

        [Test]
        public void StoreRawTrue()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            Currency currency3 = new("BTC");
            Currency currency4 = new("DOT");            
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2, currency3, currency4 });

            //WHEN
            currencyMatrix.Store(currency2, currency4, 4);
            Decimal sotredExchange = (Decimal)currencyMatrix.GetMatrix().Rows[1][4];

            //THEN
            Assert.AreEqual(4, sotredExchange);
        }

        [Test]
        public void StoreDoubleWaysExchangeTrue()
        {
            //GIVEN
            Currency currency1 = new ("ADA");
            Currency currency2 = new ("USDT");
            Currency currency3 = new ("BTC");
            Currency currency4 = new ("DOT");
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2, currency3 , currency4 });

            //WHEN
            currencyMatrix.Store(currency2, currency4, 4);
            Decimal sotredExchange =(Decimal) currencyMatrix.GetMatrix().Rows[1][4] ;
            Decimal autoCalculatedExchange =(Decimal) currencyMatrix.GetMatrix().Rows[3][2] ;

            //THEN
            Assert.AreEqual(4,sotredExchange) ;
            Assert.AreEqual(0.25, autoCalculatedExchange);
        }
        
        [Test]
        public void StoreWrongDecimalFormatFalse()
        {
            //GIVEN
            Currency currency1 = new ("ADA");
            Currency currency2 = new ("USDT");
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2});

            //WHEN
            currencyMatrix.Store(currency1, currency2, 1/3);
           var notStored = currencyMatrix.GetMatrix().Rows[0][2] ;

            //THEN
            Assert.AreEqual(DBNull.Value, notStored);
        }

        [Test]
        public void StoreValidFormatTrue()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2 });

            //WHEN
            currencyMatrix.Store(currency1, currency2, 1.52142M);
            var notStored = currencyMatrix.GetMatrix().Rows[0][2];

            //THEN
            Assert.AreEqual(1.5214M, notStored);
        }

        [Test]
        public void StoreValidAutoCalculatedFormatTrue()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            currencyMatrix.InitCurrencyResume(new List<Currency> { currency1, currency2 });

            //WHEN
            currencyMatrix.Store(currency1, currency2, 1.52142M);
            var notStored = currencyMatrix.GetMatrix().Rows[1][1];

            //THEN
            Assert.AreEqual(0.6573M, notStored);
        }
    }
}
