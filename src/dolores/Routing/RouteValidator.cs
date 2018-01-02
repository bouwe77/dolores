using Dolores.Exceptions;

namespace Dolores.Routing
{
   internal class RouteValidator
   {
      internal static void Validate(string uriTemplate)
      {
         bool isUriTemplateValid = uriTemplate != null;
         if (!isUriTemplateValid)
         {
            throw new DoloresConfigurationException("Invalid URI template");
         }
      }
   }
}