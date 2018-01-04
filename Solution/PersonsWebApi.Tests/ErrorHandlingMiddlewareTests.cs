using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using PersonsWebApi.Middleware;
using Xunit;

namespace PersonsWebApi.Tests
{
  public class ErrorHandlingMiddlewareTests
  {
    private readonly Mock<ILoggerFactory> _mockLoggerFactory;
    private readonly Mock<ILogger> _mockLogger;

    private static readonly Exception TestException = new Exception("Test Exception");
    private readonly RequestDelegate _successAction = c => Task.CompletedTask;
    private readonly RequestDelegate _failAction = c => throw TestException;

    public ErrorHandlingMiddlewareTests()
    {
      _mockLoggerFactory = new Mock<ILoggerFactory>();
      _mockLogger = new Mock<ILogger>();
      _mockLoggerFactory.Setup(x => x.CreateLogger("ErrorHandlingMiddleware")).Returns(_mockLogger.Object);
    }

    [Fact]
    public async Task WhenPipelineActionThrows_ShouldSetInternalServerError()
    {
      // Arrange
      var target = new ErrorHandlingMiddleware(_failAction, _mockLoggerFactory.Object);
      var context = new DefaultHttpContext();

      // Act

      await target.Invoke(context);

      // Assert
      Assert.Equal(500, context.Response.StatusCode);
    }

    [Fact]
    public async Task WhenPipelineActionThrows_ShouldLogException()
    {
      // Arrange
      var target = new ErrorHandlingMiddleware(_failAction, _mockLoggerFactory.Object);

      // Act

      await target.Invoke(new DefaultHttpContext());

      // Assert
      _mockLogger.Verify(x => x.Log(
        LogLevel.Error,
        It.IsAny<EventId>(),
        It.IsAny<object>(),
        TestException,
        It.IsAny<Func<object, Exception, string>>()));
    }

    [Fact]
    public async Task WhenPipelineActionSucceed_ShouldNotLogError()
    {
      // Arrange
      var target = new ErrorHandlingMiddleware(_successAction, _mockLoggerFactory.Object);

      // Act
      await target.Invoke(new DefaultHttpContext());

      // Assert
      _mockLogger.Verify(x => x.Log(
        It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.IsAny<object>(),
        It.IsAny<Exception>(),
        It.IsAny<Func<object, Exception, string>>()), Times.Never);
    }
  }
}
