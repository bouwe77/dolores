using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolores.Uris
{
   /// <summary>
   /// Parses a configured URI template to a regular expression to make it suitable for matching against rquest URIs.
   /// </summary>
   internal class UriTemplateToRegexConverter
   {
      public static string Convert(string uriTemplate)
      {
         // The URI template "*" matches any URI.
         string regex = UriTemplateRegexes.AnyUri;
         if (uriTemplate != "*")
         {
            // Get rid of leading and trailing slashes.
            uriTemplate = uriTemplate.Trim('/');
            regex = "^$";

            // Split the URI template on the remaining slashes, if any.
            var uriTemplateParts = uriTemplate.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (uriTemplateParts.Any())
            {
               var regexParts = new List<string>();

               foreach (var part in uriTemplateParts)
               {
                  string trimmedPart = part.Trim();
                  if (trimmedPart.StartsWith("{") && trimmedPart.EndsWith("}"))
                  {
                     // If the part is surrounded by curly brackets, the regex for that part is any character except the forward slash.
                     regexParts.Add(UriTemplateRegexes.ResourceName);
                  }
                  else
                  {
                     // Otherwise the regex for that part is exactly the string itself.
                     regexParts.Add($"({trimmedPart})");
                  }
               }

               // Add all parts to the regex.
               regex = string.Join(@"\/", regexParts);
               regex = $"^{regex}$";
            }
         }

         return regex;
      }
   }
}
