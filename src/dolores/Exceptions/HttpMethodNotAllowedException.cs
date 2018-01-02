using Dolores.Http;

namespace Dolores.Exceptions
{
   /// <summary>
   /// This exception results in an HTTP 405 Method Not Allowed response is returned to the client.
   /// </summary>
   /// <seealso cref="HttpException" />
   public class HttpMethodNotAllowedException : HttpException
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="HttpMethodNotAllowedException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public HttpMethodNotAllowedException(string message)
         : base(message, HttpStatusCode.MethodNotAllowed)
      {
      }
   }
}
