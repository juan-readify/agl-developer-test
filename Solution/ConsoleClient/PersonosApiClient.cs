using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleClient
{
  class PersonosApiClient

  {
    private readonly Uri ApiBaseUri = new Uri("http://localhost:53539/api/");

    private HttpClient GetClient()
    {
      var client = new HttpClient
      {
        BaseAddress = ApiBaseUri
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
