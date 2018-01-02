using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dolores;
using Dolores.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestApp
{
   public class Startup
   {
      private IConfigurationRoot _configuration;

      public Startup(IHostingEnvironment env)
      {
         var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("doloresSettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"doloresSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
         _configuration = builder.Build();
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddOptions();
         services.Configure<MyTEMPSettings>(_configuration.GetSection("dolores"));
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
         loggerFactory.AddConsole();

         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseDoloresHttpHandlerMiddleware();
      }
   }
}
