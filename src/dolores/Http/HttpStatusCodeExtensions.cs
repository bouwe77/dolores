using System.Text.RegularExpressions;

namespace Dolores.Http
{
   internal static class HttpStatusCodeExtensions
   {
      public static string GetReasonPhrase(this HttpStatusCode httpStatusCode)
      {
         string enumName = httpStatusCode.ToString();

         string reasonPhrase = "OK";
         if (httpStatusCode != HttpStatusCode.Ok)
         {
            reasonPhrase = AddSpacesBeforeUpperCase(enumName);

            // Replace some specific words.
            reasonPhrase = reasonPhrase.Replace("Http", "HTTP");
            reasonPhrase = reasonPhrase.Replace("Non Authoritative", "Non-Authoritative");
            reasonPhrase = reasonPhrase.Replace("Request Uri", "Request-URI");
         }

         return reasonPhrase;
      }

      private static string AddSpacesBeforeUpperCase(string text)
      {
         string newValue = Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");
         return newValue;
      }
   }
}
