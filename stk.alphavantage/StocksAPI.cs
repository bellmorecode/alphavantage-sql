using Microsoft.SqlServer.Server;
using System;
using System.IO;
using System.Net;

namespace stk.alphavantage
{
    public static class StocksAPI
    {
        [SqlFunction(DataAccess = DataAccessKind.Read, SystemDataAccess = SystemDataAccessKind.Read)]
        public static string GetQuote(string symbol)
        {
            string fn = "GLOBAL_QUOTE";
            return APICall($"{baseUrl}?function={fn}&symbol={symbol}&apikey={__NextAPIKey()}");
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, SystemDataAccess = SystemDataAccessKind.Read)]
        public static string GetIntradaySeries(string symbol)
        {
            string interval = "5min";
            string fn = "TIME_SERIES_INTRADAY";
            return APICall($"{baseUrl}?function={fn}&symbol={symbol}&interval={interval}&apikey={__NextAPIKey()}");
        }
        private static string APICall(string url)
        {
            string output = string.Empty;
            try
            {
                var req = HttpWebRequest.CreateHttp(url);
                using (var resp = req.GetResponse())
                {
                    output = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

        #region API Keys
        private static string __NextAPIKey()
        {
            var nextKey = api_keys[api_pos++];
            if (api_pos == api_keys.Length) { api_pos = 0; }
            return nextKey;
        }
        static int api_pos = 0;
        static string[] api_keys = new[] { "KDU2A8QDR8AD6KC5", "0VVZDI4MNL1Y3771", "3OK5PJT6LA9K2EX7" };
        // NOTE: replace the strings above with your keys, can use more than 1. or just use mine, whatever!
        #endregion

        const string baseUrl = "https://www.alphavantage.co/query";
    }
}