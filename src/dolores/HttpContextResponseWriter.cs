using Dolores.Helpers;
using Dolores.Http;
using Dolores.Responses;

namespace Dolores
{
   internal class HttpContextResponseWriter
   {
      private readonly IHttpContextWrapper _wrappedHttpContext;

      public HttpContextResponseWriter(IHttpContextWrapper wrappedHttpContext)
      {
         _wrappedHttpContext = wrappedHttpContext;
      }

      public void WriteResponse(Response response)
      {
         _wrappedHttpContext.ClearResponse();

         int statusCodeValue = (int) response.StatusCode;
         if (statusCodeValue > 0)
         {
            _wrappedHttpContext.ResponseStatusCode = statusCodeValue;
         }

         string statusDescription = response.StatusCode.GetReasonPhrase();
         if (!string.IsNullOrWhiteSpace(statusDescription))
         {
            _wrappedHttpContext.ResponseStatusDescription = statusDescription;
         }

         var headers = response.Headers.ToNameValueCollection();
         if (headers != null)
         {
            foreach (string headerName in headers)
            {
               var headerValue = headers[headerName];
               _wrappedHttpContext.ResponseHeaders.Add(headerName, headerValue);
            }
         }

         _wrappedHttpContext.OutputStream = response.MessageBody;
      }
   }
}
