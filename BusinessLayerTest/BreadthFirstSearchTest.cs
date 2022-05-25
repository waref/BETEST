using Engine.IServices;
using Engine.Models;
using Engine.Services;
using Engine.Statics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayerTest
{
    public class BreadthFirstSearchTest
    {
        [Test]
        public void ShortestRoadIntTestWithEmptyVerticesFalse()
        {
            //GIVEN
            int[] vertices = Array.Empty<int>();
            Tuple<int, int>[] edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(5,6), Tuple.Create(6,7), Tuple.Create(6,8)};

            Graph<int> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            int startVertex = 1;

            //WHEN
            algorithms.ShortestPathFunction(graph, startVertex);

            //THEN

            Assert.AreEqual(ExecutionStates.KO, algorithms.GetExecutionResult(out string message));
            Assert.AreEqual(Static.WRONG_INVALIDGRAPH, message);
        }

        [Test]
        public void ShortestRoadIntTestFalse()
        {
            //GIVEN
            int[] vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8, };
            Tuple<int, int>[] edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,6),
                Tuple.Create(5,6), Tuple.Create(6,7), Tuple.Create(6,2)};

            Graph<int> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            int startVertex = 1;

            //WHEN
            Func<int, IEnumerable<int>> shortestPathTo = algorithms.ShortestPathFunction(graph, startVertex);

            //THEN
            IEnumerable<int> path = shortestPathTo(8);

            Assert.AreEqual(startVertex, path.First());
            Assert.AreEqual(1, path.Count());
        }

        [Test]
        public void ShortestRoadIntTestForwardTrue()
        {
            //GIVEN
            IEnumerable<int> expectedShortRoad = new[] { 1, 3, 5, 8 };
            int[] vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Tuple<int, int>[] edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(5,6), Tuple.Create(6,7), Tuple.Create(6,8)};

            Graph<int> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            int startVertex = 1;

            //WHEN
            Func<int, IEnumerable<int>> shortestPathTo = algorithms.ShortestPathFunction(graph, startVertex);

            //THEN
            IEnumerable<int> path = shortestPathTo(8);

            Assert.IsTrue(Enumerable.SequenceEqual(expectedShortRoad, path));
        }

        [Test]
        public void ShortestRoadIntBackwordTestTrue()
        {
            //GIVEN
            IEnumerable<int> expectedShortRoad = new[] { 8, 5, 3, 1 };
            int[] vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Tuple<int, int>[] edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(5,6), Tuple.Create(6,7), Tuple.Create(6,8)};

            Graph<int> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            int startVertex = 8;

            //WHEN
            Func<int, IEnumerable<int>> shortestPathTo = algorithms.ShortestPathFunction(graph, startVertex);

            //THEN
            IEnumerable<int> path = shortestPathTo(1);

            Assert.IsTrue(Enumerable.SequenceEqual(expectedShortRoad, path));
        }

        [Test]
        public void ShortestRoadCurrenciesNoPathTestForwardFalse()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            Currency currency3 = new("BTC");
            Currency currency4 = new("DOT");
            Currency currency5 = new("XRP");
            Currency currency6 = new("LTC");
            Currency[] vertices = new[] { currency1, currency2, currency3, currency4, currency5, currency6 };

            Tuple<Currency, Currency>[] edges = new[]{Tuple.Create(currency1,currency2), Tuple.Create(currency1,currency3),
                Tuple.Create(currency2,currency4), Tuple.Create(currency3,currency4)};

            Graph<Currency> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            Currency startVertex = currency1;

            //WHEN
            Func<Currency, IEnumerable<Currency>> shortestPathTo = algorithms.ShortestPathFunction(graph, startVertex);

            //THEN
            _ = shortestPathTo(currency5);

            Assert.AreEqual(ExecutionStates.NoPathFoud, algorithms.GetExecutionResult(out string _));
        }
        [Test]
        public void ShortestRoadCurrenciesTestInvalidForCalculationFalse()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency5 = new("XRP");
            BreadthFirstSearch algorithms = new();
            Currency startVertex = currency1;

            //WHEN
            Func<Currency, IEnumerable<Currency>> shortestPathTo = algorithms.ShortestPathFunction(null, startVertex);

            //THEN
            IEnumerable<Currency> path = shortestPathTo(currency5);

            Assert.AreEqual(0, path.Count());
        }

        [Test]
        public void ShortestRoadCurrenciesTestForwardTrue()
        {
            //GIVEN
            Currency currency1 = new("ADA");
            Currency currency2 = new("USDT");
            Currency currency3 = new("BTC");
            Currency currency4 = new("DOT");
            Currency currency5 = new("XRP");
            Currency currency6 = new("LTC");
            IEnumerable<Currency> expectedShortRoad = new[] { currency1,  currency3,  currency5 };
            Currency[] vertices = new[] { currency1, currency2, currency3, currency4, currency5, currency6 };

            Tuple<Currency, Currency>[] edges = new[]{Tuple.Create(currency1,currency2), Tuple.Create(currency1,currency3),
                Tuple.Create(currency2,currency4), Tuple.Create(currency3,currency4), Tuple.Create(currency3,currency5)};

            Graph<Currency> graph = new(vertices, edges);
            BreadthFirstSearch algorithms = new();
            Currency startVertex = currency1;

            //WHEN
            Func<Currency, IEnumerable<Currency>> shortestPathTo = algorithms.ShortestPathFunction(graph, startVertex);

            //THEN
            IEnumerable<Currency> path = shortestPathTo(currency5);

            Assert.IsTrue(Enumerable.SequenceEqual(expectedShortRoad, path));
        }

    }   
}