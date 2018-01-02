using System;

namespace Dolores.Configuration
{
   /// <summary>
   /// 
   /// </summary>
   [Flags]
   public enum ErrorResponseDetails
   {
      /// <summary>
      /// Never show error details in the response.
      /// </summary>
      None = 0,
      /// <summary>
      /// Show error details in the response in case of client errors i.e. 4xx HTTP Status Code.
      /// </summary>
      Client = 1 << 0,
      /// <summary>
      /// Show error details in the response in case of server errors i.e. 5xx HTTP Status Code.
      /// </summary>
      Server = 1 << 1,
      /// <summary>
      /// Always show all error details in the response.
      /// </summary>
      All = Client | Server
   }
}
