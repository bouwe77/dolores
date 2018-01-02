using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// This exception results in an HTTP 400 Bad Request response is returned to the client.
   /// </summary>
   /// <seealso cref="HttpException" />
   public class HttpBadRequestException : HttpException
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="HttpBadRequestException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public HttpBadRequestException(string message)
         : base(message, HttpStatusCode.BadRequest)
      {
      }
   }
}
