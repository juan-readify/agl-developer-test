using System.Collections.Generic;
using System.Threading.Tasks;
using PersonsApi;

namespace PersonsWebApi.Services
{
  public interface IPersonsService
  {
    Task<IEnumerable<CatsByOwnerGender>> GetCatsByOwnerGenderAsync();
    Task<IEnumerable<Person>> GetPersonsAsync();
  }
}