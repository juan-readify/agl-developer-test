using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleClient
{
  internal class PersonsApiClient

  {
    private readonly Uri _apiBaseUri = new Uri("http://localhost:53539/api/");

    private HttpClient GetClient()
    {
      var client = new HttpClient
      {
        BaseAddress = _apiBaseUri
      };

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      return client;
    }

    public string GetPersons()
    {
      using (var client = GetClient())
      {
        return client.GetStringAsync("person").Result;
      }
    }

    public string GetCats()
    {
      using (var client = GetClient())
      {
        return client.GetStringAsync("person/catsbyownergender").Result;
      }
    }
  }
}