using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PersonsApi
{
  public class PersonsClient : IPersonsClient
  {
    private readonly HttpClient _httpClient;

    public PersonsClient(Uri baseAddress)
    {
      _httpClient = new HttpClient
      {
        BaseAddress = baseAddress
      };
      _httpClient.DefaultRequestHeaders.Accept.Clear();
      _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<Person[]> GetPersonsAsync()
    {
      var response = await _httpClient.GetAsync("people.json");
      response.EnsureSuccessStatusCode();
      var result = JsonConvert.DeserializeObject<Person[]>(await response.Content.ReadAsStringAsync());
      return result;
    }
  }
}