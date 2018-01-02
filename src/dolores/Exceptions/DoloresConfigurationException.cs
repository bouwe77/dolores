using System;
using Dolores.Configuration;

namespace Dolores.Exceptions
{
   /// <summary>
   /// Exceptions that occur because of configuration errors.
   /// </summary>
   /// <seealso cref="DoloresException" />
   /// <seealso cref="DoloresConfig"/>
   public class DoloresConfigurationException : DoloresException
   {
      internal DoloresConfigurationException(string message)
         : base(message)
      {
      }

      internal DoloresConfigurationException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
