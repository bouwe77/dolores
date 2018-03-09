using System.Linq;

namespace Dolores.Routing
{
   internal static class RouteExtensions
   {
      public static string GetUriWithParameterValues(this Route route, params string[] uriParams)
      {
         string uri = null;

         if (route != null && uriParams != null)
         {
            uri = route.UriTemplate;

            var uriTemplateSegments = route.UriTemplate.Split('/');
            var uriTemplateParams = uriTemplateSegments.Where(x => x.StartsWith("{") && x.EndsWith("}")).ToList();

            bool uriParamsValid = uriParams.Length == uriTemplateParams.Count;
            if (uriParamsValid && uriTemplateParams.Any())
            {
               int index = 0;
               foreach (var uriTemplateParam in uriTemplateParams)
               {
                  uri = uri.Replace(uriTemplateParam, uriParams[index]);
                  index++;
               }
            }
         }

         return uri;
      }
   }
}
