using System.Threading.Tasks;

namespace PersonsApi
{
  public interface IClient
  {
    Task<Person[]> GetPersonsAsync();
  }
}