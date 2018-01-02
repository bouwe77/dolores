using Microsoft.AspNetCore.Http;
using System.IO;

namespace Dolores
{
   internal interface IHttpContextWrapper
   {
      bool HasFormContentType { get; }
      IHeaderDictionary RequestHeaders { get; }
      IHeaderDictionary ResponseHeaders { get; }
      string RequestQueryString { get; }
      IFormCollection RequestForm { get; }
      string RequestHostAndPort { get; }
      string RequestHttpMethod { get; }
      Stream InputStream { get; }
      Stream OutputStream { set; }
      string RequestProtocol { get; }
      string RawRequestUrl { get; }
      int ResponseStatusCode { get; set; }
      string ResponseStatusDescription { get; set; }
      void ClearResponse();
   }
}
