using System;
using System.IO;
using Dolores.Exceptions;
using Newtonsoft.Json;

namespace Dolores.Requests
{
   /// <summary>
   /// Extension methods for the <see cref="Stream"/> class.
   /// </summary>
   public static class StreamExtensions
   {
      /// <summary>
      /// Deserializes the JSON stream to an object of the given generic type. 
      /// </summary>
      /// <typeparam name="T">The generic type to deserialize the JSON stream to.</typeparam>
      /// <param name="stream">The stream to deserialize.</param>
      /// <returns>The object of the given type.</returns>
      /// <exception cref="HttpBadRequestException">Thrown when deserializing failed.</exception>
      public static T DeserializeJson<T>(this Stream stream)
      {
         T value;
         bool success = stream.TryDeserializeJson(out value);

         if (!success)
         {
            throw new HttpBadRequestException("Message body is invalid JSON");
         }

         return value;
      }

      /// <summary>
      /// Tries to deserialize the JSON stream to an object of the given generic type. 
      /// </summary>
      /// <typeparam name="T">The generic type to deserialize the JSON stream to.</typeparam>
      /// <param name="stream">The stream to deserialize.</param>
      /// <param name="deserializedObject">The object of the given type, or null when deserialization failed.</param>
      /// <returns>True if derserialization was successful, otherwise false.</returns>
      public static bool TryDeserializeJson<T>(this Stream stream, out T deserializedObject)
      {
         bool success = false;
         deserializedObject = default(T);

         if (stream != null)
         {
            var streamReader = new StreamReader(stream);
            var content = streamReader.ReadToEnd();

            try
            {
               deserializedObject = JsonConvert.DeserializeObject<T>(content);
               success = true;
            }
            catch (Exception)
            {
               //TODO Logger.Instance.Warn($"Deserializing the JSON stream failed: {exception}");
            }
         }

         return success;
      }
   }
}
