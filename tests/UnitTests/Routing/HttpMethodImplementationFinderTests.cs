using Dolores.Http;
using Dolores.Requests;
using Dolores.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests.Routing
{
   [TestClass]
   public class HttpMethodImplementationFinderTests
   {
      [TestMethod]
      public void FindImplementation_ReturnsNull_WhenNoImplementationsAreDefined()
      {
         var request = CreateRequest(HttpMethod.Get);
         var route = CreateRoute();

         var finder = new HttpMethodImplementationFinder();
         var implementation = finder.FindImplementation(request, route);

         Assert.IsNull(implementation);
      }

      [TestMethod]
      public void FindImplementation_ReturnsNull_WhenImplementationForRequestMethodIsNotDefined()
      {
         var request = CreateRequest(HttpMethod.Get);
         var route = CreateRoute(HttpMethod.Post);

         var finder = new HttpMethodImplementationFinder();
         var implementation = finder.FindImplementation(request, route);

         Assert.IsNull(implementation);
      }

      [TestMethod]
      public void FindImplementation_ReturnsImplementation_ForAllSupportedMethods_WhenRequestAndRouteContainSingleMethod()
      {
         var httpMethods = new List<HttpMethod>
         {
            HttpMethod.Get,
            HttpMethod.Post,
            HttpMethod.Put,
            HttpMethod.Delete,
            HttpMethod.Patch,
            HttpMethod.Head,
            HttpMethod.Options,
         };

         foreach (var httpMethod in httpMethods)
         {
            var request = CreateRequest(httpMethod);
            var route = CreateRoute(httpMethod);

            var finder = new HttpMethodImplementationFinder();
            var implementation = finder.FindImplementation(request, route);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(httpMethod, implementation.HttpMethod);
         }
      }

      [TestMethod]
      public void FindImplementation_ReturnsImplementation_WhenRouteHasMultipleMethodsDefined()
      {
         var request = CreateRequest(HttpMethod.Patch);
         var route = CreateRoute(HttpMethod.Get, HttpMethod.Post, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Patch, HttpMethod.Head, HttpMethod.Options);

         var finder = new HttpMethodImplementationFinder();
         var implementation = finder.FindImplementation(request, route);

         Assert.IsNotNull(implementation);
         Assert.AreEqual(HttpMethod.Patch, implementation.HttpMethod);
      }

      private Route CreateRoute(params HttpMethod[] httpMethods)
      {
         var route = new Route("whatever", "whatever");

         foreach (var httpMethod in httpMethods)
         {
            route.AddImplementation(httpMethod, "whatever,whatever", "whatever");
         }

         return route;
      }

      private Request CreateRequest(HttpMethod httpMethod)
      {
         var request = new Request("whatever", "whatever", "whatever");
         request.Method = httpMethod;
         return request;
      }
   }
}
