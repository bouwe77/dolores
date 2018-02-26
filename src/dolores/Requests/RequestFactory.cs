using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Dolores.Requests
{
   internal class RequestFactory
   {
      public static Request Create(IHttpContextWrapper wrappedHttpContext)
      {
         string serverUrl = $"{wrappedHttpContext.RequestProtocol}://{wrappedHttpContext.RequestHostAndPort}";

         var queryString = QueryHelpers.ParseQuery(wrappedHttpContext.RequestQueryString);

         var requestHeaders = new Dictionary<string, StringValues>();
         foreach (var requestHeader in wrappedHttpContext.RequestHeaders)
         {
            requestHeaders.Add(requestHeader.Key, requestHeader.Value);
         }

         var form = new Dictionary<string, StringValues>();
         if (wrappedHttpContext.HasFormContentType)
         {
            foreach (var formItem in wrappedHttpContext.RequestForm)
            {
               form.Add(formItem.Key, formItem.Value);
            }
         }

         var request = new Request(serverUrl, wrappedHttpContext.RawRequestUrl, wrappedHttpContext.RequestHttpMethod)
         {
            QueryString = queryString,
            Headers = requestHeaders,
            Form = form
         };

         wrappedHttpContext.InputStream.CopyTo(request.MessageBody);
         request.MessageBody.Position = 0;

         return request;
      }
   }
}
