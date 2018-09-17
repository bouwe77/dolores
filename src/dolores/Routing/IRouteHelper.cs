namespace Dolores.Routing
{
   public interface IRouteHelper
   {
      /// <summary>
      /// Gets the URI by route identifier from the config.
      /// </summary>
      /// <param name="routeIdentifier">The route identifier as defined in the route config.</param>
      /// <param name="parameterValues">The parameter values to apply to the route URI.</param>
      string GetRouteUriByRouteIdentifier(string routeIdentifier, params string[] parameterValues);
   }
}
