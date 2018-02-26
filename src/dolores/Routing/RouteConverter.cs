using Dolores.Configuration;
using System.Collections.Generic;

namespace Dolores.Routing
{
   /// <summary>
   /// Converts routes from config (<see cref="Configuration.Route"/>) to <see cref="Route"/>s which are used in Dolores to handle requests.
   /// </summary>
   internal class RouteConverter
   {
      public static Dictionary<string, Route> Convert(List<Configuration.Route> routesFromConfig)
      {
         var routeFactory = new RouteFactory();

         var convertedRoutes = new Dictionary<string, Route>();

         foreach (var routeFromConfig in routesFromConfig)
         {
            var route = routeFactory.Create(routeFromConfig.UriTemplate);
            AddImplementation(routeFromConfig, route);
            convertedRoutes.Add(routeFromConfig.Identifier, route);
         }

         return convertedRoutes;
      }

      private static void AddImplementation(Configuration.Route routeFromConfig, Route route)
      {
         if (routeFromConfig.Get != null)
         {
            route.AddImplementation(Http.HttpMethod.Get, routeFromConfig.Get.Type, routeFromConfig.Get.Method);
         }

         if (routeFromConfig.Post != null)
         {
            route.AddImplementation(Http.HttpMethod.Post, routeFromConfig.Post.Type, routeFromConfig.Post.Method);
         }

         if (routeFromConfig.Put != null)
         {
            route.AddImplementation(Http.HttpMethod.Put, routeFromConfig.Put.Type, routeFromConfig.Put.Method);
         }

         if (routeFromConfig.Delete != null)
         {
            route.AddImplementation(Http.HttpMethod.Delete, routeFromConfig.Delete.Type, routeFromConfig.Delete.Method);
         }

         if (routeFromConfig.Patch != null)
         {
            route.AddImplementation(Http.HttpMethod.Patch, routeFromConfig.Patch.Type, routeFromConfig.Patch.Method);
         }

         if (routeFromConfig.Head != null)
         {
            route.AddImplementation(Http.HttpMethod.Head, routeFromConfig.Head.Type, routeFromConfig.Head.Method);
         }

         if (routeFromConfig.Options != null)
         {
            route.AddImplementation(Http.HttpMethod.Options, routeFromConfig.Options.Type, routeFromConfig.Options.Method);
         }
      }
   }
}
