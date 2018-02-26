using System.Collections.Generic;
using System.Collections.Specialized;

namespace Dolores.Helpers
{
   /// <summary>
   /// Extension methods for the <see cref="Dictionary{TKey,TValue}"/> class.
   /// </summary>
   public static class DictionaryExtensions
   {
      /// <summary>
      /// Returns a default value of type TValue if the key does not exist in the dictionary        
      /// </summary>
      /// <param name="dictionary">The dictionary to search</param>
      /// <param name="key">Key to search for</param>
      /// <param name="onMissing">Optional default value of type TValue. If not specified, the C# default value will be returned.</param>
      public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue onMissing = default(TValue))
      {
         return dictionary.TryGetValue(key, out var value) ? value : onMissing;
      }

      /// <summary>
      /// Converts a <see cref="Dictionary{TKey,TValue}"/> to a <see cref="NameValueCollection"/>.
      /// </summary>
      /// <param name="dictionary">The dictionary.</param>
      /// <returns>The NameValueCollection.</returns>
      public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> dictionary)
      {
         var nameValueCollection = new NameValueCollection();

         foreach (var dictionaryItem in dictionary)
         {
            nameValueCollection.Add(dictionaryItem.Key, dictionaryItem.Value);
         }

         return nameValueCollection;
      }
   }
}
