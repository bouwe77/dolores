using Dolores.Uris;

namespace Dolores.Routing
{
   internal class RouteFactory
   {
      public Route Create(string uriTemplate)
      {
         RouteValidator.Validate(uriTemplate);

         var uriTemplateRegexPattern = UriTemplateToRegexConverter.Convert(uriTemplate);

         var route = new Route(uriTemplate, uriTemplateRegexPattern);

         return route;
      }
   }
}