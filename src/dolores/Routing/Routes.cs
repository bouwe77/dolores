//using System;
//using System.Collections.Generic;

//namespace Dolores.Routing
//{
//   internal class Routes
//   {
//      private readonly Dictionary<string, Route> _routes = new Dictionary<string, Route>();

//      private static readonly Lazy<Routes> Lazy = new Lazy<Routes>(() => new Routes());

//      public static Routes Instance => Lazy.Value;

//      private Routes()
//      {
//      }

//      public Route AddRoute(string routeIdentifier, string uriTemplate)
//      {
//         var routeFactory = new RouteFactory();

//         var route = routeFactory.Create(uriTemplate);

//         if (_routes.ContainsKey(routeIdentifier))
//         {
//            var existingRoute = _routes[routeIdentifier];

//            // Overwrite route if routeIdentifier already exists but with a different uriTemplate.
//            // If the route already existed with the same uriTemplate, no new route is added and the existing route is returned.
//            bool overwrite = !existingRoute.UriTemplate.Equals(uriTemplate, StringComparison.OrdinalIgnoreCase);
//            if (overwrite)
//            {
//               _routes[routeIdentifier] = route;
//            }
//            else
//            {
//               route = existingRoute;
//            }
//         }
//         else
//         {
//            _routes.Add(routeIdentifier, route);
//         }

//         return route;
//      }

//      public Dictionary<string, Route> GetAllRoutes()
//      {
//         return _routes;
//      }

//      public Route GetRouteByIdentifier(string routeIdentifier)
//      {
//         Route route = null;

//         if (!string.IsNullOrWhiteSpace(routeIdentifier))
//         {
//            _routes.TryGetValue(routeIdentifier, out route);
//         }

//         return route;
//      }

//      /// <summary>
//      /// Clears all routes, for unit testing only.
//      /// </summary>
//      internal void ClearRoutes()
//      {
//         _routes.Clear();
//      }
//   }
//}