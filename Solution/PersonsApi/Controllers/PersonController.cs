using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonsApi;
using PersonsWebApi.Services;

namespace PersonsWebApi.Controllers
{
  [Produces("application/json")]
  [Route("api/person")]
  public class PersonController : Controller
  {
    private readonly IPersonsService _personsService;

    public PersonController(IPersonsService personsService)
    {
      _personsService = personsService ?? throw new ArgumentNullException(nameof(personsService));
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> GetPersonsAsync()
    {
      return await _personsService.GetPersonsAsync();
    }

    [HttpGet]
    [Route("byownergender")]
    public async Task<IEnumerable<CatsByOwnerGender>> GetCatsByOwnerGenderAsync()
    {
      return await _personsService.GetCatsByOwnerGenderAsync();
    }
  }
}