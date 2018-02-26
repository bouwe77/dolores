using System;
using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// Base class for exceptions that result in HTTP error responses being returned to the client.
   /// </summary>
   /// <seealso cref="DoloresException" />
   public class HttpException : DoloresException
   {
      /// <summary>
      /// Gets or sets the HTTP status code.
      /// </summary>
      /// <value>The HTTP status code.</value>
      public HttpStatusCode StatusCode { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="HttpException"/> class.
      /// </summary>
      /// <param name="exceptionMessage">The exception message.</param>
      /// <param name="statusCode">The HTTP status code.</param>
      public HttpException(string exceptionMessage, HttpStatusCode statusCode)
         : base(exceptionMessage)
      {
         StatusCode = statusCode;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="HttpException"/> class.
      /// </summary>
      /// <param name="exceptionMessage">The exception message.</param>
      /// <param name="statusCode">The HTTP status code.</param>
      /// <param name="innerException">The inner exception.</param>
      public HttpException(string exceptionMessage, HttpStatusCode statusCode, Exception innerException)
         : base(exceptionMessage, innerException)
      {
         StatusCode = statusCode;
      }
   }
}