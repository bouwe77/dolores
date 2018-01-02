using Dolores.Requests;

namespace Dolores.Routing
{
   internal interface IHttpMethodImplementationFinder
   {
      IHttpMethodImplementation FindImplementation(Request request, Route route);
   }
}