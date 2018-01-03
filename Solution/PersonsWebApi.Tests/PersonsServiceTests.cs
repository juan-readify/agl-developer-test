using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PersonsApi;
using PersonsWebApi.Services;
using Xunit;

namespace PersonsWebApi.Tests
{
  public class PersonsServiceTests
  {
    private readonly Mock<IPersonsClient> _mockClient;
    private readonly PersonsService _target;

    private readonly Person[] _testData = new[]
    {
      new Person { Gender = Gender.Male, Pets = new []
      {
        new Pet { Type = PetType.Fish, Name = "Rainbow"},
        new Pet { Type = PetType.Cat, Name = "Piero" }
      }},
      new Person { Gender = Gender.Female, Pets = new []
      {
        new Pet { Type = PetType.Cat, Name = "Zapper" },
        new Pet { Type = PetType.Cat, Name = "Austin"}

      }},
      new Person { Gender = Gender.Male, Pets = new []
        {
          new Pet { Type = PetType.Dog, Name = "Zapper" },
          new Pet { Type = PetType.Fish, Name = "Goldie"}
        }},
      new Person { Gender = Gender.Male, Pets = new []
      {
        new Pet { Type = PetType.Cat, Name = "Piero"},
        new Pet { Type = PetType.Cat, Name = "Winter"}
      }},
      new Person { Gender = Gender.Male }
    };

    public PersonsServiceTests()
    {
      _mockClient = new Mock<IPersonsClient>();

      _mockClient.Setup(x => x.GetPersonsAsync()).Returns(Task.FromResult(_testData));

      _target = new PersonsService(_mockClient.Object);
    }

    [Fact]
    public void WhenCreatingWithNullClient_ShouldThrow()
    {
      Assert.Throws<ArgumentNullException>(() => new PersonsService(null));
    }

    [Fact]
    public async Task GetPersonsAsync_ReturnsDataFromApi()
    {
      // Arrange
      var expected = new Person[0];
      _mockClient.Setup(x => x.GetPersonsAsync()).Returns(Task.FromResult(expected));

      // Act
      var result = await _target.GetPersonsAsync();

      // Assert
      Assert.Same(expected, result);
    }

    [Fact]
    public async Task GetPersonsAsync_WhenResultIsNull_ShoudReturnEmpty()
    {
      // Arrange
      _mockClient.Setup(x => x.GetPersonsAsync()).Returns(Task.FromResult(default(Person[])));

      // Act
      var result = await _target.GetPersonsAsync();

      // Assert
      Assert.False(result.Any());
    }


    [Fact]
    public async Task GetCatsByOwnerGender_ShouldOnlyReturnCats()
    {
      // Act
      var result = await _target.GetCatsByOwnerGenderAsync();

      // Assert
      var names = result.SelectMany(x => x.CatNames);
      Assert.False(names.Except(new[] { "Piero", "Zapper", "Austin", "Winter" }).Any());
    }

    [Fact]
    public async Task GetCatsByOwnerGender_ShouldOnlyReturnCatsGroupedByOwnerGender()
    {
      // Act
      var result = await _target.GetCatsByOwnerGenderAsync();

      // Assert
      var ofMale = result.Where(x => x.OwnerGender == "Male").SelectMany(x => x.CatNames);
      var ofFemale = result.Where(x => x.OwnerGender == "Female").SelectMany(x => x.CatNames);

      Assert.False(ofMale.Except(new[] { "Piero", "Winter" }).Any());
      Assert.False(ofFemale.Except(new[] { "Austin", "Zapper" }).Any());
    }

    [Fact]
    public async Task GetCatsByOwnerGender_ShouldOnlyReturnCatsNamesAlphabeticallyOrdered()
    {
      // Act
      var result = await _target.GetCatsByOwnerGenderAsync();

      // Assert
      var ofMale = result.Where(x => x.OwnerGender == "Male").SelectMany(x => x.CatNames);
      var ofFemale = result.Where(x => x.OwnerGender == "Female").SelectMany(x => x.CatNames);

      Assert.Collection(ofMale, x => Assert.Equal("Piero", x), x => Assert.Equal("Winter", x));
      Assert.Collection(ofFemale, x => Assert.Equal("Austin", x), x => Assert.Equal("Zapper", x));
    }

    [Fact]
    public async Task GetCatsByOwnerGender_ShouldOnlyReturnUniqueCatsNames()
    {
      // Act
      var result = await _target.GetCatsByOwnerGenderAsync();

      // Assert

      Assert.DoesNotContain(result.SelectMany(x => x.CatNames).GroupBy(x => x).Select(x => x.Count()), x => x != 1);

    }
  }
}
