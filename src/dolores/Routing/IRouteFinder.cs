namespace Dolores.Routing
{
   internal interface IRouteFinder
   {
      Route FindRoute(string requestUri);
      Route GetRouteByIdentifier(string routeIdentifier);
   }
}