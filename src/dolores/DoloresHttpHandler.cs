using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Dolores.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dolores
{
   public class DoloresHttpHandler
   {
      private readonly ILoggerFactory _loggerFactory;
      private readonly Settings _settings;

      public DoloresHttpHandler(RequestDelegate next, IOptions<Settings> settings, ILoggerFactory loggerFactory)
      {
         _loggerFactory = loggerFactory;
         _settings = settings.Value;
      }

      public async Task Invoke(HttpContext httpContext)
      {
         var logger = _loggerFactory.CreateLogger<DoloresHttpHandler>();
         logger.LogInformation($"=======> Hello World");

         var wrappedHttpContext = new HttpContextWrapper(httpContext);
         var implementation = new DoloresHttpHandlerImplementation(_settings, _loggerFactory);
         await implementation.HandleAsync(wrappedHttpContext);
      }
   }
}
