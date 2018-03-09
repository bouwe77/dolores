using Dolores.Configuration;
using Dolores.Requests;
using Dolores.Routing;
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
      public Request Request { get; internal set; }

      /// <summary>
      /// Gets or sets the logger.
      /// </summary>
      public ILogger Logger { get; set; }

      /// <summary>
      /// Gets or sets the Dolores settings.
      /// </summary>
      public DoloresSettings DoloresSettings { get; internal set; }

      /// <summary>
      /// Gets or sets the <see cref="RouteHelper"/>.
      /// </summary>
      public RouteHelper RouteHelper { get; internal set; }
   }
}
