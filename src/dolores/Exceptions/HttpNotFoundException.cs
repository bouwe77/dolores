using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// This exception results in an HTTP 404 Not Found response is returned to the client.
   /// </summary>
   /// <seealso cref="HttpException" />
   public class HttpNotFoundException : HttpException
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public HttpNotFoundException(string message)
         : base(message, HttpStatusCode.NotFound)
      {
      }
   }
}
