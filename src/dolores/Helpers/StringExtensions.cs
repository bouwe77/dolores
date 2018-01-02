using System.IO;

namespace Dolores.Helpers
{
   internal static class StringExtensions
   {
      public static Stream ToStream(this string stringValue)
      {
         var stream = new MemoryStream();
         var writer = new StreamWriter(stream);
         writer.Write(stringValue);
         writer.Flush();
         stream.Position = 0;

         return stream;
      }
   }
}
