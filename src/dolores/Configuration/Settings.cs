//using System;
//using System.Collections.Generic;
//using Dolores.Requests;
//using Dolores.Responses;
//using Newtonsoft.Json;
//using Dolores.Routing;

//namespace Dolores.Configuration
//{
//   internal class Settings : ISettings
//   {
//      private static readonly Lazy<Settings> Lazy = new Lazy<Settings>(() => new Settings());

//      private JsonSerializerSettings _jsonSerializerSettings;

//      public static Settings Instance => Lazy.Value;

//      private Settings()
//      {
//         // Apply default JSON serializer settings.
//         _jsonSerializerSettings = new JsonSerializerSettings
//         {
//            Formatting = Formatting.Indented,
//            NullValueHandling = NullValueHandling.Ignore
//         };

//         OnBeforeSendResponse = new List<Action<Request, Response>>();
//      }

//      public ErrorResponseDetails ErrorDetailsInResponses { get; set; }

//      public JsonSerializerSettings JsonSerializerSettings
//      {
//         get
//         {
//            return _jsonSerializerSettings;
//         }
//         set
//         {
//            if (value != null)
//            {
//               _jsonSerializerSettings = value;
//            }
//         }
//      }

//      public List<Action<Request, Response>> OnBeforeSendResponse { get; set; }

//      private readonly Dictionary<string, Route> _routes = new Dictionary<string, Route>();

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
