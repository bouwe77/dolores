using System.Collections.Generic;
using Dolores.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolores.Routing
{
   [TestClass]
   public class RouteFinderTests
   {
      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsEmptyAndIsConfigured()
      {
         string requestUri = string.Empty;
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsOneWordAndIsConfigured()
      {
         string requestUri = "word";
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsOneWordAndIsConfigured_InDifferentCasing()
      {
         string requestUri = "wOrD";
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsOneWordAndIdAndIsConfigured()
      {
         string requestUri = "word/123";
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsTwoWordsAndOneIdAndIsConfigured()
      {
         string requestUri = "word/123/morestuff";
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsImplementation_WhenRequestUriIsTwoWordsAndOneIdAndIsConfigured_InDifferentCasing()
      {
         string requestUri = "WoRd/123/mOrEsTuFf";
         var route = FindRoute(requestUri);

         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsNothing_WhenRequestUriIsNotConfigured_1()
      {
         string requestUri = "word/123/morestuff/moio";
         var route = FindRoute(requestUri);

         Assert.IsNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsNothing_WhenRequestUriIsNotConfigured_2()
      {
         string requestUri = "fdshfjhsjkhfjk";
         var route = FindRoute(requestUri);

         Assert.IsNull(route);
      }

      [TestMethod]
      public void FindRoute_ReturnsNothing_WhenNoRoutesAreConfigured()
      {
         var routeFinder = new RouteFinder(new List<Configuration.Route>());

         string requestUri = "hello";
         var route = routeFinder.FindRoute(requestUri);

         Assert.IsNull(route);
      }

      [TestMethod]
      public void GetRouteByIdentifier_ReturnsRoute_WhenIdentifierConfigured()
      {
         var routeFinder = GetRouteFinder();
         var route = routeFinder.GetRouteByIdentifier("1");
         Assert.IsNotNull(route);
      }

      [TestMethod]
      public void GetRouteByIdentifier_ReturnsNothing_WhenIdentifierNotConfigured()
      {
         var routeFinder = GetRouteFinder();
         var route = routeFinder.GetRouteByIdentifier("666");
         Assert.IsNull(route);
      }

      private Route FindRoute(string requestUri)
      {
         var routeFinder = GetRouteFinder();
         return routeFinder.FindRoute(requestUri);
      }

      private RouteFinder GetRouteFinder()
      {
         // The Httproute is not used by the RouteFinder class, so although the properties are required, the values do not matter.
         Configuration.HttpMethodImplementation dummyImplementation = new Configuration.HttpMethodImplementation
         {
            Type = "lorem, ipsum",
            Method = "lorem ipsum"
         };

         // Define the routes (UriTemplates) that are necessary for this test.
         var routes = new List<Configuration.Route>
         {
            new Configuration.Route { UriTemplate = "", Get = dummyImplementation, Identifier = "1" },
            new Configuration.Route { UriTemplate = "word", Get = dummyImplementation, Identifier = "2" },
            new Configuration.Route { UriTemplate = "word/{id}", Get = dummyImplementation, Identifier = "3" },
            new Configuration.Route { UriTemplate = "word/{id}/morestuff", Get = dummyImplementation, Identifier = "4" },
         };

         return new RouteFinder(routes);
      }
   }
}
