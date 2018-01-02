using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Dolores.Helpers
{
   internal static class IEnumerableExtensions
   {
      public static bool HasDuplicates<T>(this IEnumerable<T> list)
      {
         var hashset = new HashSet<T>();
         return list.Any(t => !hashset.Add(t));
      }

      public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> list)
      {
         return list.Where(x => x != null);
      }
   }
}