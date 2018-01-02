using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Dolores.Http;

namespace Dolores.Routing
{
   internal class Route
   {
      public string UriTemplate { get; }

      public Regex UriTemplateRegex { get; }

      public List<IHttpMethodImplementation> Implementations { get; }

      internal Route(string uriTemplate, string uriTemplateRegexPattern)
      {
         UriTemplate = uriTemplate;
         UriTemplateRegex = new Regex(uriTemplateRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
         Implementations = new List<IHttpMethodImplementation>();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="httpMethod"></param>
      /// <param name="className"></param>
      /// <param name="methodName"></param>
      public void AddImplementation(HttpMethod httpMethod, string className, string methodName)
      {
         var implementation = HttpMethodImplementationFactory.Create(httpMethod, className, methodName);

         // Overwrite the Implementation if it already exists with the same HTTP method.
         var existingImplementation = Implementations.FirstOrDefault(x => x.HttpMethod == implementation.HttpMethod);
         if (existingImplementation != null)
         {
            Implementations.Remove(existingImplementation);
         }

         Implementations.Add(implementation);
      }

      public override string ToString()
      {
         var stringBuilder = new StringBuilder();

         stringBuilder.Append("[");
         stringBuilder.Append($"UriTemplate = '{UriTemplate.Replace("{", "{{").Replace("}", "}}")}', ");
         stringBuilder.Append($"UriTemplateRegex = '{UriTemplateRegex.ToString().Replace("{", "{{").Replace("}", "}}")}'");
         stringBuilder.Append("]");

         return stringBuilder.ToString();
      }
   }
}
