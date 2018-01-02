using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolores.Helpers
{
   internal static class Enforce
   {
      public static T Condition<T>(T argument, bool condition, string description)
      {
         if (!condition)
         {
            throw new ArgumentException(description);
         }

         return argument;
      }

      public static IEnumerable<T> ListNotEmpty<T>(IEnumerable<T> argument, string description)
      {
         return Condition(argument, argument != null && argument.Any(), description);
      }

      public static T ArgumentNotNull<T>(T argument, string description) where T : class
      {
         return Condition(argument, argument != null, description);
      }

      public static string StringNotNullOrEmpty(string argument, string description)
      {
         return Condition(argument, !string.IsNullOrEmpty(argument), description);
      }
   }
}
