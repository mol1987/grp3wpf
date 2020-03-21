using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace Library
{
    public class ArticleProcessor
    {
        public async Task<List<TypeLib.Article>> LoadArticle()
        {
            string url = "";
            url = "http://api.jkb.zone/articles";

            ApiHelper.InitializeClient();

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Reponse as string
                    string stringResponse = await response.Content.ReadAsStringAsync();

                    // Convert into list of JSON -object
                    List<TypeLib.Article> article = JsonConvert.DeserializeObject<List<TypeLib.Article>>(stringResponse);

                    return article;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
