using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolores.Exceptions;
using Dolores.Responses;
using Dolores.Routing;
using Microsoft.Extensions.Logging;

namespace Dolores.Requests
{
   internal class RequestHandler
   {
      private readonly Request _request;
      private readonly IHttpMethodImplementation _httpMethodImplementation;
      private readonly ILoggerFactory _loggerFactory;
      private ILogger<RequestHandler> _logger;

      public RequestHandler(Request request, IHttpMethodImplementation httpMethodImplementation, ILoggerFactory loggerFactory)
      {
         _request = request;
         _httpMethodImplementation = httpMethodImplementation;
         _loggerFactory = loggerFactory;
         _logger = loggerFactory.CreateLogger<RequestHandler>();
      }

      public async Task<Response> HandleAsync()
      {
         var doloresHandler = DoloresHandlerFactory.CreateDoloresHandler(_httpMethodImplementation, _request, _loggerFactory);
         var response = await GetResponseAsync(doloresHandler);

         _logger.LogDebug(response.ToString());

         return response;
      }

      private async Task<Response> GetResponseAsync(DoloresHandler doloresHandler)
      {
         var methodArguments = ConvertArguments(_request.UriParameters.Values);

         var methodInfo = GetMethodInfo(doloresHandler, methodArguments.Length);

         Response response;
         bool methodIsSync = methodInfo.ReturnType == typeof(Response);
         bool methodIsAsync = methodInfo.ReturnType == typeof(Task<Response>);

         if (methodIsSync)
         {
            response = (Response)InvokeMethod(doloresHandler, methodInfo, methodArguments);
         }
         else if (methodIsAsync)
         {
            var result = (Task<Response>)InvokeMethod(doloresHandler, methodInfo, methodArguments);
            response = await result;
         }
         else
         {
            throw new HttpNotImplementedException($"Method {methodInfo.Name} must either return type {typeof(Response).Name} or {typeof(Task<Response>).Name}");
         }

         return response;
      }

      private static object InvokeMethod(DoloresHandler doloresHandler, MethodInfo methodInfo, object[] methodArguments)
      {
         try
         {
            var result = methodInfo.Invoke(doloresHandler, methodArguments);
            return result;
         }
         catch (Exception exception)
         {
            if (exception.InnerException != null)
            {
               throw exception.InnerException;
            }

            throw;
         }
      }

      private MethodInfo GetMethodInfo(DoloresHandler doloresHandler, int numberOfArguments)
      {
         string configuredMethod = $"Configured method '{_httpMethodImplementation.MethodName}'";

         // At this moment every argument must be of type string.
         var types = new Type[numberOfArguments];
         for (int i = 0; i < numberOfArguments; i++)
         {
            types[i] = typeof(string);
         }

         MethodInfo method;
         try
         {
            method = doloresHandler.GetType().GetMethod(_httpMethodImplementation.MethodName, types);
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"{configuredMethod} is invalid: {exception.Message}", exception);
         }

         if (method == null)
         {
            throw new HttpNotImplementedException($"{configuredMethod} is not a public instance method on class '{_httpMethodImplementation.FullyQualifiedClassName}' and/or does not have matching arguments");
         }

         return method;
      }

      private static object[] ConvertArguments(IEnumerable<string> uriArguments)
      {
         return uriArguments.Select(element => (object)element).ToArray();
      }
   }
}
