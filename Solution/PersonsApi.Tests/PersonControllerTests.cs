using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PersonsApi;
using PersonsWebApi.Controllers;
using PersonsWebApi.Services;
using Xunit;

namespace PersonsWebApi.Tests
{
    public class PersonControllerTests
    {
      private readonly PersonController _target;
      private readonly Mock<IPersonsService> _mockPersonService;

      public PersonControllerTests()
      {
        _mockPersonService = new Mock<IPersonsService>();
        _target = new PersonController(_mockPersonService.Object);
      }

      [Fact]
      public void Ctor_WhenNullPersonService_ShouldThrow()
      {
        Assert.Throws<ArgumentNullException>(() => new PersonController(null));
      }

      [Fact]
      public async Task GetPersonsAsync_ReturnsDataFromService()
      {
        // Arrange
        var expected = (IEnumerable<Person>)new Person[1];
        _mockPersonService.Setup(x => x.GetPersonsAsync()).Returns(Task.FromResult(expected));

        // Act
        var result =  await _target.GetPersonsAsync();

        // Assert
        Assert.Same(expected, result);
      }

      [Fact]
      public async Task GetCatsByOwnerGenderAsync_ReturnsDataFromService()
      {
        // Arrange
        var expected = (IEnumerable<CatsByOwnerGender>)new CatsByOwnerGender[1];
        _mockPersonService.Setup(x => x.GetCatsByOwnerGenderAsync()).Returns(Task.FromResult(expected));

        // Act
        var result =  await _target.GetCatsByOwnerGenderAsync();

        // Assert
        Assert.Same(expected, result);
      }


    }
}
