using System.Threading.Tasks;

namespace PersonsApi
{
  public interface IPersonsClient
  {
    Task<Person[]> GetPersonsAsync();
  }
}