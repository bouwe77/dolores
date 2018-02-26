using Dolores.Requests;
using Microsoft.Extensions.Logging;

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
      public Request Request { get; set; }

      /// <summary>
      /// Gets or sets the logger.
      /// </summary>
      public ILogger Logger { get; set; }
   }
}
