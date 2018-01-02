namespace Dolores.Http
{
   /// <summary>
   /// HTTP request header fields.
   /// Source: https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
   /// </summary>
   public class HttpRequestHeaderFields
   {
      public const string Accept = "Accept";
      public const string AcceptCharset = "Accept-Charset";
      public const string AcceptEncoding = "Accept-Encoding";
      public const string AcceptLanguage = "Accept-Language";
      public const string AcceptDatetime = "Accept-Datetime";
      public const string Authorization = "Authorization";
      public const string CacheControl = "Cache-Control";
      public const string Connection = "Connection";
      public const string Cookie = "Cookie";
      public const string ContentLength = "Content-Length";
      public const string ContentMd5 = "Content-MD5";
      public const string ContentType = "Content-Type";
      public const string Date = "Date";
      public const string Expect = "Expect";
      public const string Forwarded = "Forwarded";
      public const string From = "From";
      public const string Host = "Host";
      public const string IfMatch = "If-Match";
      public const string IfModifiedSince = "If-Modified-Since";
      public const string IfNoneMatch = "If-None-Match";
      public const string IfRange = "If-Range";
      public const string IfUnmodifiedSince = "If-Unmodified-Since";
      public const string MaxForwards = "Max-Forwards";
      public const string Origin = "Origin";
      public const string Pragma = "Pragma";
      public const string ProxyAuthorization = "Proxy-Authorization";
      public const string Range = "Range";
      public const string Referer = "Referer";
      public const string Te = "TE";
      public const string UserAgent = "User-Agent";
      public const string Upgrade = "Upgrade";
      public const string Via = "Via";
      public const string Warning = "Warning";
   }
}
