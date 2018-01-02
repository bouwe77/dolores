using System;

namespace Dolores.Exceptions
{
   /// <summary>
   /// Exceptions that occur during serialization. 
   /// </summary>
   /// <seealso cref="DoloresException" />
   public class DoloresSerializationException : DoloresException
   {
      internal DoloresSerializationException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
