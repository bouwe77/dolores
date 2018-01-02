using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolores.Uris
{
   internal class UriParameterParser
   {
      public static Dictionary<string, string> Parse(string uriTemplate, string requestUri)
      {
         var uriParameters = new Dictionary<string, string>();

         var uriTemplateParts = uriTemplate.Split(new [] { '/' }, StringSplitOptions.RemoveEmptyEntries);
         var requestUriParts = requestUri.Split(new [] { '/' }, StringSplitOptions.RemoveEmptyEntries);
         if (uriTemplateParts.Any())
         {
            int index = 0;
            foreach (var uriTemplatePart in uriTemplateParts)
            {
               if (!string.IsNullOrWhiteSpace(uriTemplatePart) 
                  && uriTemplatePart.StartsWith("{") 
                  && uriTemplatePart.EndsWith("}"))
               {
                  string parameterName = uriTemplatePart.Trim('{').Trim('}');
                  string parameterValue = requestUriParts[index];
                  uriParameters.Add(parameterName, parameterValue);
               }

               index++;
            }
         }

         return uriParameters;
      }
   }
}
