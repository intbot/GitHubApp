using GitHubApp.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubApp
{
    public class ApiInterface
    {
        public List<Repo> GetRepos()
        {
            var repos = new List<Repo>();

            try
            {
                var thirtyDaysBack = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                var url = new Uri($"http://api.github.com/search/repositories?q=created:%3E={thirtyDaysBack}+stars:%3E=100&sort=stars");
                //var url = new Uri("https://api.myjson.com/bins/enph7");

                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36");

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var root = JsonConvert.DeserializeObject<RootObject>(result);

                        repos = root.items.Select(i => new Repo
                        {
                            FullName = i.full_name,
                            Description = i.description,
                            Owner = i.owner.login,
                            Stars = i.stargazers_count,
                            AvatarUrl = i.owner.avatar_url
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return repos;
        }
    }
}

