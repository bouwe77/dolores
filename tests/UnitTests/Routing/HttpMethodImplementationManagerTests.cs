using Dolores.Exceptions;
using Dolores.Http;
using Dolores.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dolores.Routing
{
   [TestClass]
   public class HttpMethodImplementationManagerTests
   {
      private Mock<IRouteFinder> _routeFinderMock;
      private Mock<IHttpMethodImplementationFinder> _httpMethodImplementationFinderMock;
      private readonly Request _dummyRequest = new Request("whatever", "whatever", "whatever");
      private HttpMethodImplementationManager _manager;

      [TestInitialize]
      public void TestInitialize()
      {
         _routeFinderMock = new Mock<IRouteFinder>(MockBehavior.Strict);
         _httpMethodImplementationFinderMock = new Mock<IHttpMethodImplementationFinder>(MockBehavior.Strict);
         _manager = new HttpMethodImplementationManager(_routeFinderMock.Object, _httpMethodImplementationFinderMock.Object);
      }

      [TestCleanup]
      public void TestCleanup()
      {
         _routeFinderMock.VerifyAll();
         _httpMethodImplementationFinderMock.VerifyAll();
      }

      [TestMethod]
      public void GetImplementation_ReturnsImplementation_WhenItExists()
      {
         var dummyRoute = new Route("whatever", "whatever");
         _routeFinderMock
            .Setup(x => x.FindRoute(It.IsAny<string>()))
            .Returns(dummyRoute);

         var dummyImplementation = new HttpMethodImplementation(HttpMethod.Get, "whatever", "whatever", "whatever");
         _httpMethodImplementationFinderMock
            .Setup(x => x.FindImplementation(It.IsAny<Request>(), It.IsAny<Route>()))
            .Returns(dummyImplementation);

         var implementation = _manager.GetImplementation(_dummyRequest);

         Assert.IsNotNull(implementation);
      }

      [TestMethod]
      [ExpectedException(typeof(HttpNotFoundException))]
      public void GetImplementation_ThrowsNotFoundException_WhenUriTemplateDoesNotExist()
      {
         _routeFinderMock
            .Setup(x => x.FindRoute(It.IsAny<string>()))
            .Returns((Route) null);

         _manager.GetImplementation(_dummyRequest);
      }

      [TestMethod]
      [ExpectedException(typeof(HttpMethodNotAllowedException))]
      public void GetImplementation_ThrowsNotImplementedException_WhenUriTemplateExistsButMethodDoesNot()
      {
         var dummyRoute = new Route("whatever", "whatever");
         _routeFinderMock
            .Setup(x => x.FindRoute(It.IsAny<string>()))
            .Returns(dummyRoute);

         _httpMethodImplementationFinderMock
            .Setup(x => x.FindImplementation(It.IsAny<Request>(), It.IsAny<Route>()))
            .Returns((HttpMethodImplementation) null);

         _manager.GetImplementation(_dummyRequest);
      }
   }
}
