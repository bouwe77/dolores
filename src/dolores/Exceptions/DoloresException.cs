using System;

namespace Dolores.Exceptions
{
   /// <summary>
   /// Base class for all Dolores related exceptions.
   /// </summary>
   /// <seealso cref="System.Exception" />
   public abstract class DoloresException : Exception
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DoloresException"/> class.
      /// </summary>
      /// <param name="message">The message that describes the error.</param>
      protected DoloresException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="DoloresException"/> class.
      /// </summary>
      /// <param name="message">The error message that explains the reason for the exception.</param>
      /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
      protected DoloresException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
