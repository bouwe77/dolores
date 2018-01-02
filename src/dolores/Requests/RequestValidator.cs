using System;
using Dolores.Exceptions;
using Dolores.Http;

namespace Dolores.Requests
{
   internal class RequestValidator
   {
      public void Validate(Request request)
      {
         // Parse the HTTP method.
         HttpMethod httpMethod;
         bool parseSuccessful = Enum.TryParse(request.MethodAsString, true, out httpMethod);
         if (!parseSuccessful)
         {
            throw new HttpMethodNotAllowedException($"The HTTP method '{request.MethodAsString.ToUpper()}' is not allowed");
         }

         request.Method = httpMethod;

         //TODO Logger.Instance.Debug(request.ToString());
      }
   }
}
