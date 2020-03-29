using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Library;


namespace Tests
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var data = await new ArticleProcessor().LoadArticle();
            

            foreach (var repo in data)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine();
            }
            Console.Write("Type any key to quit..");
            Console.ReadKey();
        }
        //private static async Task<List<Repository>> ProcessRepositories()
        //{
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        //    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        //    var streamTask = client.GetStreamAsync("http://api.jkb.zone/articles");
        //    var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
        //    return repositories;
        //}
    }
}
