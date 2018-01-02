using System;
using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// This exception results in an HTTP 501 Not Implemented response is returned to the client.
   /// </summary>
   /// <seealso cref="HttpException" />
   public class HttpNotImplementedException : HttpException
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="HttpNotImplementedException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public HttpNotImplementedException(string message)
         : base(message, HttpStatusCode.NotImplemented)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="HttpNotImplementedException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      /// <param name="innerException">The inner exception.</param>
      public HttpNotImplementedException(string message, Exception innerException)
         : base(message, HttpStatusCode.NotImplemented, innerException)
      {
      }
   }
}
