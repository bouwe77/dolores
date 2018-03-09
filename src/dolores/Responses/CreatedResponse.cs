using Dolores.Http;

namespace Dolores.Responses
{
   public class CreatedResponse : Response
   {
      public CreatedResponse(string locationUri)
        : base(HttpStatusCode.Created)
      {
         SetHeader(HttpResponseHeaderFields.Location, locationUri);
      }
   }
}
