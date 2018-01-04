using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PersonsWebApi.Middleware
{
  public class ErrorHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory )
    {
      _next = next;
      _logger = loggerFactory.CreateLogger("ErrorHandlingMiddleware");
    }

    public async Task Invoke(HttpContext context /* other scoped dependencies */)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Exception executing action.");
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      var code = HttpStatusCode.InternalServerError; // 500 if unexpected

      var result = JsonConvert.SerializeObject(new {error = exception.Message});
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int) code;
      return context.Response.WriteAsync(result);
    }
  }
}