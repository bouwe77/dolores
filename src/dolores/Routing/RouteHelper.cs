using System;
using System.Collections.Generic;
using System.Text;
using Dolores.Configuration;

namespace Dolores.Routing
{
   public class RouteHelper
   {
      private readonly DoloresSettings _settings;

      public RouteHelper(DoloresSettings settings)
      {
         _settings = settings;
      }

      /// <summary>
      /// Gets the URI by route identifier from the config.
      /// </summary>
      /// <param name="routeIdentifier">The route identifier as defined in the route config.</param>
      /// <param name="parameterValues">The parameter values to apply to the route URI.</param>
      public string GetRouteUriByRouteIdentifier(string routeIdentifier, params string[] parameterValues)
      {
         var routeFinder = new RouteFinder(_settings.Routes);
         var route = routeFinder.GetRouteByIdentifier(routeIdentifier);
         var uri = route.GetUriWithParameterValues(parameterValues);

         return uri;
      }
   }
}
