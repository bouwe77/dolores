using Dolores.Exceptions;
using Dolores.Requests;
using Dolores.Uris;

namespace Dolores.Routing
{
   internal class HttpMethodImplementationManager
   {
      private readonly IRouteFinder _routeFinder;
      private readonly IHttpMethodImplementationFinder _implementationFinder;

      public HttpMethodImplementationManager(IRouteFinder routeFinder)
         : this(routeFinder, new HttpMethodImplementationFinder())
      {
      }

      internal HttpMethodImplementationManager(IRouteFinder routeFinder, IHttpMethodImplementationFinder implementationFinder)
      {
         _routeFinder = routeFinder;
         _implementationFinder = implementationFinder;
      }

      public IHttpMethodImplementation GetImplementation(Request request)
      {
         // Determine whether the request URI is defined as route.
         var route = _routeFinder.FindRoute(request.Uri);
         if (route == null)
         {
            throw new HttpNotFoundException($"No route configured for URI '{request.RawUrl}'");
         }

         // Determine whether the HTTP method from the request is defined in the Route.
         var methodImplementation = _implementationFinder.FindImplementation(request, route);
         if (methodImplementation == null)
         {
            throw new HttpMethodNotAllowedException($"No Route configured for an HTTP {request.Method.ToString().ToUpper()} on URI '{request.RawUrl}'");
         }

         request.UriParameters = UriParameterParser.Parse(route.UriTemplate, request.Uri);

         return methodImplementation;
      }
   }
}
