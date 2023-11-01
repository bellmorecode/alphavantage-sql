using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace stk.alphavantage.tests
{
    [TestClass]
    public class StocksAPITests
    {
        readonly string ticker_symbol = "AMD";

        [TestMethod]
        public void GetQuote_Test()
        {
            var result = StocksAPI.GetQuote(ticker_symbol);
            Assert.IsNotNull(result);
            Trace.TraceInformation(result);
        }

        [TestMethod]
        public void GetIntradaySeries_Test()
        {
            var result = StocksAPI.GetIntradaySeries(ticker_symbol);
            Assert.IsNotNull(result);
            Trace.TraceInformation(result);
        }
    }
}
