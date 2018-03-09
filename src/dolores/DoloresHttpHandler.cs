using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Dolores.Configuration;
using Microsoft.Extensions.Logging;

namespace Dolores
{
   public class DoloresHttpHandler
   {
      private readonly ILogger _logger;
      private readonly DoloresSettings _settings;
      private readonly ILoggerFactory _loggerFactory;

      public DoloresHttpHandler(RequestDelegate next, DoloresSettings settings, ILoggerFactory loggerFactory)
      {
         _loggerFactory = loggerFactory;
         _logger = loggerFactory.CreateLogger<DoloresHttpHandler>();
         _settings = settings;
      }

      public async Task Invoke(HttpContext httpContext)
      {
         _logger.LogInformation($"=======> Hello World");

         var wrappedHttpContext = new HttpContextWrapper(httpContext);
         var implementation = new DoloresHttpHandlerImplementation(_settings, _loggerFactory);
         await implementation.HandleAsync(wrappedHttpContext);
      }
   }
}
