namespace Dolores.Uris
{
   internal class UriTemplateRegexes
   {
      /// <summary>
      /// Match anything.
      /// </summary>
      public const string AnyUri = ".*?";

      /// <summary>
      /// Match everything except the forward slash.
      /// </summary>
      public const string ResourceName = @"([^\/]+)";
   }
}
