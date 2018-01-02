using System.Collections.Generic;
using System.Collections.Specialized;

namespace Dolores.Helpers
{
   internal static class NameValueCollectionExtensions
   {
      public static Dictionary<string, string> ToDictionary(this NameValueCollection nameValueCollection)
      {
         var dictionary = new Dictionary<string, string>();

         // Copies all items to the dictionary and if the key already exists appends the value comma separated to the existing value.
         foreach (string key in nameValueCollection.Keys)
         {
            if (!dictionary.ContainsKey(key))
            {
               dictionary.Add(key, nameValueCollection[key]);
            }
            else
            {
               dictionary[key] += ", " + nameValueCollection[key];
            }
         }

         return dictionary;
      }
   }
}
