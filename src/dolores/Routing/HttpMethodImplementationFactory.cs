using System;
using Dolores.Exceptions;
using Dolores.Helpers;
using Dolores.Http;

namespace Dolores.Routing
{
   internal class HttpMethodImplementationFactory
   {
      public static IHttpMethodImplementation Create(HttpMethod httpMethod, string type, string method)
      {
         Enforce.StringNotNullOrEmpty(type, nameof(type));
         Enforce.StringNotNullOrEmpty(method, nameof(method));

         var splittedType = type.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

         if (splittedType.Length != 2)
         {
            throw new DoloresConfigurationException("The configured Type must be a string value containing the fully qualified class name and the assembly name separated by a comma.");
         }

         string fullyQualifiedClassName = splittedType[0];
         string assemblyName = splittedType[1];

         IHttpMethodImplementation httpMethodImplementation = new HttpMethodImplementation(httpMethod, assemblyName, fullyQualifiedClassName, method);

         return httpMethodImplementation;
      }
   }
}
