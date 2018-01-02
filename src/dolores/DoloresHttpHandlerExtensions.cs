using Microsoft.AspNetCore.Builder;

namespace Dolores
{
   public static class DoloresHtppHandlerExtensions
   {
      public static IApplicationBuilder UseDoloresHttpHandlerMiddleware(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<DoloresHttpHandler>();
      }
   }
}
