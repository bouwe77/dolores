using System;
using System.Threading;
using Dolores.Exceptions;
using Dolores.Http;
using Microsoft.Extensions.Logging;

namespace Dolores.Requests
{
   internal class RequestValidator
   {
      private ILogger<RequestValidator> _logger;

      public RequestValidator(ILoggerFactory loggerFactory)
      {
         _logger = loggerFactory.CreateLogger<RequestValidator>();
      }

      public void Validate(Request request)
      {
         // Parse the HTTP method.
         bool parseSuccessful = Enum.TryParse(request.MethodAsString, true, out HttpMethod httpMethod);
         if (!parseSuccessful)
         {
            throw new HttpMethodNotAllowedException($"The HTTP method '{request.MethodAsString.ToUpper()}' is not allowed");
         }

         request.Method = httpMethod;

         _logger.LogDebug(request.ToString());
      }
   }
}
