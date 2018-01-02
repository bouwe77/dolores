using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dolores.Helpers;
using Dolores.Http;

namespace Dolores.Responses
{
   /// <summary>
   /// Contains all information of the HTTP response that will be returned to the client.
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   public class Response : IDisposable
   {
      /// <summary>
      /// Gets the HTTP status code.
      /// </summary>
      /// <value>The HTTP status code.</value>
      public HttpStatusCode StatusCode { get; }

      /// <summary>
      /// Gets the HTTP headers.
      /// </summary>
      /// <value>The HTTP headers.</value>
      public Dictionary<string, string> Headers { get; }

      /// <summary>
      /// Gets the message body.
      /// </summary>
      /// <value>The message body.</value>
      public Stream MessageBody { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Response"/> class.
      /// </summary>
      /// <param name="statusCode">The HTTP status code.</param>
      public Response(HttpStatusCode statusCode)
      {
         StatusCode = statusCode;
         Headers = new Dictionary<string, string>();
      }

      /// <summary>
      /// Sets the header.
      /// </summary>
      /// <param name="name">The header name.</param>
      /// <param name="value">The header value.</param>
      public void SetHeader(string name, string value)
      {
         Enforce.StringNotNullOrEmpty(name, "Header name");

         if (Headers.ContainsKey(name))
         {
            Headers[name] = value;
         }
         else
         {
            Headers.Add(name, value);
         }
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            MessageBody?.Dispose();
         }
      }

      /// <summary>
      /// Returns a <see cref="string" /> that represents this instance.
      /// </summary>
      /// <returns>A <see cref="string" /> that represents this instance.</returns>
      public override string ToString()
      {
         var stringBuilder = new StringBuilder();

         stringBuilder.Append("[");
         stringBuilder.AppendFormat("StatusCode = '{0}', ", StatusCode);
         stringBuilder.Append("]");

         return stringBuilder.ToString();
      }
   }
}