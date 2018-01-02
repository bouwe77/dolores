using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// This exception results in an HTTP 500 Internal Server Error response is returned to the client.
   /// </summary>
   /// <seealso cref="HttpException" />
   public class HttpInternalServerErrorException : HttpException
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="HttpInternalServerErrorException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public HttpInternalServerErrorException(string message)
         : base(message, HttpStatusCode.InternalServerError)
      {
      }
   }
}
