using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dolores.Helpers;
using Dolores.Http;
using Microsoft.Extensions.Primitives;

namespace Dolores.Requests
{
   /// <summary>
   /// Contains all information of the HTTP request that was made.
   /// </summary>
   public class Request : IDisposable
   {
      internal string Uri { get; set; }

      internal string RawUrl { get; set; }

      internal string MethodAsString { get; }

      /// <summary>
      /// Gets or sets the HTTP method.
      /// </summary>
      /// <value>The HTTP method.</value>
      public HttpMethod Method { get; set; }

      internal IDictionary<string, StringValues> Headers { get; set; }

      internal IDictionary<string, StringValues> QueryString { get; set; }

      internal IDictionary<string, StringValues> Form { get; set; }

      internal IDictionary<string, string> UriParameters { get; set; }

      /// <summary>
      /// Gets the URI parameter value.
      /// The given <paramref name="parameterName"/> must correspond to the parameter name in the URI template as defined in the route.
      /// </summary>
      /// <param name="parameterName">Name of the parameter.</param>
      /// <returns>The value of the parameter, if found, otherwise <c>null</c>.</returns>
      public string GetUriParameterValue(string parameterName)
      {
         return UriParameters.GetOrDefault(parameterName);
      }

      /// <summary>
      /// Gets the HTTP header value.
      /// </summary>
      /// <param name="headerName">Name of the header.</param>
      /// <returns>The header value.</returns>
      public string GetHeaderValue(string headerName)
      {
         return Headers.GetOrDefault(headerName);
      }

      /// <summary>
      /// Gets the form value.
      /// </summary>
      /// <param name="formFieldName">Name of the form field.</param>
      /// <returns>The form value.</returns>
      public string GetFormValue(string formFieldName)
      {
         return Form.GetOrDefault(formFieldName);
      }

      /// <summary>
      /// Gets the query string value.
      /// </summary>
      /// <param name="queryStringName">Name of the query string.</param>
      /// <returns>The query string value.</returns>
      public string GetQueryStringValue(string queryStringName)
      {
         return QueryString.GetOrDefault(queryStringName);
      }

      /// <summary>
      /// Gets or sets the message body.
      /// </summary>
      /// <value>The message body.</value>
      public Stream MessageBody { get; set; }

      /// <summary>
      /// Gets or sets the server URL, e.g. http://server:port.
      /// </summary>
      public string ServerUrl { get; set; }

      internal Request(string serverUrl, string uri, string method)
      {
         RawUrl = uri;
         ServerUrl = serverUrl;

         Enforce.ArgumentNotNull(uri, nameof(uri));
         Uri = uri.Trim('/');

         MethodAsString = Enforce.StringNotNullOrEmpty(method, nameof(method));

         Headers = new Dictionary<string, StringValues>();
         QueryString = new Dictionary<string, StringValues>();
         UriParameters = new Dictionary<string, string>();
         Form = new Dictionary<string, StringValues>();

         MessageBody = new MemoryStream();
      }

      /// <summary>
      /// Returns a <see cref="String" /> that represents this instance.
      /// </summary>
      /// <returns>A <see cref="String" /> that represents this instance.</returns>
      public override string ToString()
      {
         var stringBuilder = new StringBuilder();

         stringBuilder.Append("[");
         stringBuilder.AppendFormat("Uri = '{0}', ", Uri);
         stringBuilder.AppendFormat("MethodAsString = '{0}', ", MethodAsString);
         stringBuilder.AppendFormat("Method = '{0}' ", Method);
         stringBuilder.Append("]");

         return stringBuilder.ToString();
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
   }
}