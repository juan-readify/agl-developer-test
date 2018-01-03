using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonsApi;

namespace PersonsWebApi.Services
{
  public class PersonsService : IPersonsService
  {
    private readonly IPersonsClient _personsClient;

    public PersonsService(IPersonsClient personsClient)
    {
      _personsClient = personsClient ?? throw new ArgumentNullException(nameof(personsClient));
    }

    public async Task<IEnumerable<Person>> GetPersonsAsync()
    {
      return await _personsClient.GetPersonsAsync() ?? Enumerable.Empty<Person>();
    }

    public async Task<IEnumerable<CatsByOwnerGender>> GetCatsByOwnerGenderAsync()
    {
      var persons = await GetPersonsAsync();

      var catsByOwnerGender =
        from cat in (
          // for all the persons with pets
          from person in persons
          where person.Pets != null

          // grab only those with cats
          select person
          into catPerson
          from pet in catPerson.Pets
          where pet.Type == PetType.Cat

          // and get the cat's name with its owner's gender
          select new {OwnerGender = catPerson.Gender, CatName = pet.Name})

        // so we can group and sort the cats 
        group cat by cat.OwnerGender
        into g
        select new CatsByOwnerGender
        {
          OwnerGender = g.Key.ToString(),
          CatNames = g.Select(p => p.CatName).Distinct().OrderBy(p => p).ToArray()
        };

      return catsByOwnerGender;
    }
  }
}