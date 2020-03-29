using System;
using System.Net.Http;

namespace Library
{
    /// <summary>
    /// Web Api Connector
    /// </summary>
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// Initialize connection
        ///     example: ```ApiHelper.InitializeClient()```
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            // Set expected response to be JSON
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
