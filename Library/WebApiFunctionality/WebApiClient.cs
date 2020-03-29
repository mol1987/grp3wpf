using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Library.WebApiFunctionality
{
    public class WebApiClient
    {
        static string URL = "http://localhost:1234/";
        public static async Task DoneOrderAsync(int orderNo, TypeOrder typeOrder)
        {
            using var client = new HttpClient();
            var result = await client.GetAsync(URL + typeOrder.ToString() + "?orderNo=" + orderNo);
            Console.WriteLine(result.StatusCode);
        }
    }
}
