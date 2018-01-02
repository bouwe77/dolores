using Dolores.Requests;

namespace Dolores
{
   /// <summary>
   /// Base class for all handlers you implement.
   /// </summary>
   public abstract class DoloresHandler
   {
      /// <summary>
      /// Gets or sets the request.
      /// </summary>
      /// <value>The request.</value>
      public Request Request { get; set; }
   }
}
