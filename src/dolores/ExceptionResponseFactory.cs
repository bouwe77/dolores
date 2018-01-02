using System;
using Dolores.Configuration;
using Dolores.Exceptions;
using Dolores.Http;
using Dolores.Responses;

namespace Dolores
{
   internal class ExceptionResponseFactory
   {
      private readonly Exception _exception;
      private const string ContentTypeJson = "application/json";
      private ErrorResponseDetails _errorResponseDetails;

      public ExceptionResponseFactory(Exception exception, ErrorResponseDetails errorResponseDetails)
      {
         _exception = exception;
         _errorResponseDetails = errorResponseDetails;
      }

      public Response CreateResponse()
      {
         HttpStatusCode statusCode;
         string errorMessage = null;

         // Whether details about client or server errors will be revealed in the response depends on configuration.
         bool showServerErrorDetails = (_errorResponseDetails & ErrorResponseDetails.Server) != 0;
         bool showClientErrorDetails = (_errorResponseDetails & ErrorResponseDetails.Client) != 0;

         // HttpExceptions can be thrown by both Dolores and the code using Dolores and will result in the corresponding HTTP status code.
         var httpResponseException = _exception as HttpException;
         if (httpResponseException != null)
         {
            statusCode = httpResponseException.StatusCode;
            int statusCodeValue = (int)statusCode;

            bool isClientError = statusCodeValue >= 400 && statusCodeValue < 500;
            if (isClientError && showClientErrorDetails)
            {
               errorMessage = httpResponseException.Message;
            }
            else
            {
               bool isServerError = statusCodeValue >= 500 && statusCodeValue < 600;
               if (isServerError && showServerErrorDetails)
               {
                  errorMessage = httpResponseException.Message;
               }
            }
         }
         else
         {
            // NotImplementedExceptions will result in a HTTP NotImplemented status code.
            var notImplementedException = _exception as NotImplementedException;
            if (notImplementedException != null)
            {
               var httpNotImplementedException = new HttpNotImplementedException("Not Implemented", notImplementedException);
               statusCode = httpNotImplementedException.StatusCode;

               if (showServerErrorDetails)
               {
                  errorMessage = httpNotImplementedException.Message;
               }
            }
            else
            {
               // All other exceptions will result in a InternalServerError Http status code.
               var internalServerErrorException = new HttpInternalServerErrorException(_exception.Message);
               statusCode = internalServerErrorException.StatusCode;

               if (showServerErrorDetails)
               {
                  errorMessage = internalServerErrorException.Message;
               }
            }
         }

         var response = new Response(statusCode);

         if (errorMessage != null)
         {
            var message = new { Message = errorMessage };
            response.Json(message);
         }

         return response;
      }
   }
}
