using System;
using System.Threading.Tasks;
using Dolores.Configuration;
using Dolores.Requests;
using Dolores.Responses;
using Dolores.Routing;
using Dolores.Http;
using Microsoft.Extensions.Logging;

namespace Dolores
{
   internal class DoloresHttpHandlerImplementation
   {
      private readonly DoloresSettings _settings;
      private readonly ILoggerFactory _loggerFactory;
      private readonly HttpMethodImplementationManager _implementationManager;
      private readonly ILogger _logger;

      public DoloresHttpHandlerImplementation(DoloresSettings settings, ILoggerFactory loggerFactory)
      {
         _settings = settings;
         _loggerFactory = loggerFactory;
         _implementationManager = new HttpMethodImplementationManager(new RouteFinder(settings.Routes));
         _logger = _loggerFactory.CreateLogger<DoloresHttpHandlerImplementation>();
      }

      public async Task HandleAsync(IHttpContextWrapper wrappedHttpContext)
      {
         Request request = null;
         Response response = null;

         try
         {
            request = RequestFactory.Create(wrappedHttpContext);

            _logger.LogDebug(request.ToString());

            // Determine the response by handling the request and (if necessary) handle the exception.
            try
            {
               response = await HandleRequest(request);
            }
            catch (Exception exception)
            {
               response = HandleException(exception);
            }

            //TODO Invoke all OnSendResponse actions.
            //_settings.OnBeforeSendResponse.Insert(0, MakeLocationHeaderAbsolute);
            //try
            //{
            //   foreach (var action in _settings.OnBeforeSendResponse)
            //   {
            //      action.Invoke(request, response);
            //   }
            //}
            //catch (Exception exception)
            //{
            //   logger.LogWarn($"Executing the OnBeforeSendResponse action failed: {exception}");
            //   throw;
            //}

            // Write the response to the HTTP context.
            try
            {
               var httpContextResponseWriter = new HttpContextResponseWriter(wrappedHttpContext);
               httpContextResponseWriter.WriteResponse(response);
            }
            catch (Exception exception)
            {
               _logger.LogWarning($"Writing the response failed: {exception}");
               throw;
            }
         }
         finally
         {
            request?.Dispose();
            response?.Dispose();
         }
      }

      private void MakeLocationHeaderAbsolute(Request request, Response response)
      {
         // Make the Location header URL absolute.
         if (response.Headers.ContainsKey(HttpResponseHeaderFields.Location))
         {
            string locationHeaderValue = response.Headers[HttpResponseHeaderFields.Location];
            if (!locationHeaderValue.StartsWith("http"))
            {
               response.SetHeader(HttpResponseHeaderFields.Location, $"{request.ServerUrl}{locationHeaderValue}");
            }
         }
      }

      private Response HandleException(Exception exception)
      {
         _logger.LogWarning($"Handling the request failed: {exception}");

         var exceptionResponseFactory = new ExceptionResponseFactory(exception, _settings.ErrorDetailsInResponsesEnum);
         var response = exceptionResponseFactory.CreateResponse();
         return response;
      }

      private async Task<Response> HandleRequest(Request request)
      {
         // Validate the Request.
         var requestValidator = new RequestValidator(_loggerFactory);
         requestValidator.Validate(request);

         // Find the implementation for the request.
         var methodImplementation = _implementationManager.GetImplementation(request);

         // Handle the Request with the found implementation.
         var requestHandler = new RequestHandler(request, methodImplementation, _loggerFactory, _settings);
         var response = await requestHandler.HandleAsync();

         return response;
      }
   }
}
