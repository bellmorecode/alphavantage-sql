using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace stk.alphavantage.tests
{
    [TestClass]
    public class StocksAPITests
    {
        [TestMethod]
        public void GetQuote_Test()
        {
            Trace.TraceInformation("Start Test");

            var result = StocksAPI.GetQuote("IBM");
            Assert.IsNotNull(result);

            Trace.TraceWarning(result);

            Trace.TraceInformation("Test Finished");
        }

        [TestMethod]
        public void GetIntradaySeries_Test()
        {
            Trace.TraceInformation("Start Test");

            var result = StocksAPI.GetIntradaySeries("IBM");
            Assert.IsNotNull(result);

            Trace.TraceWarning(result);

            Trace.TraceInformation("Test Finished");
        }
    }
}
