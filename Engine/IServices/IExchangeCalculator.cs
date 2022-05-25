using Engine.Models;

namespace Engine.IServices
{
    /// <summary>
    /// Exchange Calculator service process
    /// </summary>
    /// <typeparam name="T">Generic input type</typeparam>
    public interface IExchangeCalculator<in T>
    {
        /// <summary>
        /// Runcalculation process for undirected & unweighted graph using Breadth First Search algorithm
        /// </summary>
        /// <param name="token">ExchangeToken passed in each process step </param>
        /// <returns>CalculationResponse response Object</returns>
        CalculationResponse GetCalculationsResult(ExchangeToken token);
    }
}
