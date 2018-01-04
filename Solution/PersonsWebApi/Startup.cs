using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonsApi;
using PersonsWebApi.Middleware;
using PersonsWebApi.Services;

namespace PersonsWebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }


    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IPersonsClient>(new PersonsClient(new Uri(Configuration["aglApiEndpoint"])));

      services.AddTransient<IPersonsService, PersonsService>();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

      loggerFactory.AddFile($"Logs/personsWebApi-{DateTime.Now:hh_mm_ss}.log");
      app.UseMiddleware<ErrorHandlingMiddleware>();
      app.UseMvc();
    }
  }
}