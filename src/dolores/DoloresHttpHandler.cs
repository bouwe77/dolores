using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Dolores.Configuration;
using Microsoft.Extensions.Options;

namespace Dolores
{
   public class DoloresHttpHandler
   {
      private MyTEMPSettings _settings;

      public DoloresHttpHandler(RequestDelegate next, IOptions<MyTEMPSettings> settings)
      {
         settings.Value.Validate();
         _settings = settings.Value;
      }

      public async Task Invoke(HttpContext httpContext)
      {
         var wrappedHttpContext = new HttpContextWrapper(httpContext);
         var implementation = new DoloresHttpHandlerImplementation(_settings);
         await implementation.HandleAsync(wrappedHttpContext);
      }
   }
}
