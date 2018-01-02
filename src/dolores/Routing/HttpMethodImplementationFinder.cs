using Dolores.Http;
using Dolores.Requests;
using System.Linq;

namespace Dolores.Routing
{
   internal class HttpMethodImplementationFinder : IHttpMethodImplementationFinder
   {
      public IHttpMethodImplementation FindImplementation(Request request, Route route)
      {
         IHttpMethodImplementation httpMethodImplementation = null;

         switch (request.Method)
         {
            case HttpMethod.Get:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Get);
               break;
            case HttpMethod.Post:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Post);
               break;
            case HttpMethod.Put:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Put);
               break;
            case HttpMethod.Delete:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Delete);
               break;
            case HttpMethod.Patch:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Patch);
               break;
            case HttpMethod.Head:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Head);
               break;
            case HttpMethod.Options:
               httpMethodImplementation = route.Implementations.SingleOrDefault(x => x.HttpMethod == HttpMethod.Options);
               break;
         }

         return httpMethodImplementation;
      }
   }
}
