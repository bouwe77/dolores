using System.Collections.Generic;
using Dolores.Configuration;
using Dolores.Helpers;

namespace Dolores.Routing
{
   internal class RouteFinder : IRouteFinder
   {
      private readonly Dictionary<string, Route> _routes;

      public RouteFinder(List<Configuration.Route> routesFromConfig)
      {
         Enforce.ArgumentNotNull(routesFromConfig, "routesFromConfig");
         _routes = RouteConverter.Convert(routesFromConfig);
      }

      public Route GetRouteByIdentifier(string routeIdentifier)
      {
         Route route = null;

         if (!string.IsNullOrWhiteSpace(routeIdentifier))
         {
            _routes.TryGetValue(routeIdentifier, out route);
         }

         return route;
      }

      public Route FindRoute(string requestUri)
      {
         Route foundRoute = null;

         // Try to find a route by matching the request URI to the URI template regex.
         foreach (var route in _routes.Values)
         {
            var found = route.UriTemplateRegex.IsMatch(requestUri);
            if (found)
            {
               foundRoute = route;
               break;
            }
         }

         return foundRoute;
      }
   }
}
