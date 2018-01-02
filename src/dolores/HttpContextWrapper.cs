using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.IO;

namespace Dolores
{
   internal class HttpContextWrapper : IHttpContextWrapper
   {
      private HttpContext _httpContext;

      public HttpContextWrapper(HttpContext httpContext)
      {
         _httpContext = httpContext;
      }

      public IFormCollection RequestForm
      {
         get
         {
            return _httpContext.Request.Form;
         }
      }

      public string RequestQueryString
      {
         get
         {
            return _httpContext.Request.QueryString.ToString();
         }
      }

      public IHeaderDictionary RequestHeaders
      {
         get
         {
            return _httpContext.Request.Headers;
         }
      }

      public IHeaderDictionary ResponseHeaders
      {
         get
         {
            return _httpContext.Response.Headers;
         }
      }

      public string RequestHostAndPort
      {
         get
         {
            string hostAndPort = _httpContext.Request.Host.Host;
            if (_httpContext.Request.Host.Port.HasValue)
            {
               hostAndPort += $":{_httpContext.Request.Host.Port.Value}";
            }

            return hostAndPort;
         }
      }

      public string RequestHttpMethod
      {
         get
         {
            return _httpContext.Request.Method;
         }
      }

      public Stream InputStream => _httpContext.Request.Body;

      public Stream OutputStream
      {
         set
         {
            value?.CopyTo(_httpContext.Response.Body);
         }
      }

      public string RequestProtocol => _httpContext.Request.Protocol;

      public string RawRequestUrl => _httpContext.Request.Path;

      public int ResponseStatusCode
      {
         get
         {
            return _httpContext.Response.StatusCode;
         }

         set
         {
            _httpContext.Response.StatusCode = value;
         }
      }

      public string ResponseStatusDescription
      {
         get
         {
            return _httpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase;
         }

         set
         {
            _httpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = value;
         }
      }

      public bool HasFormContentType => _httpContext.Request.HasFormContentType;

      public void ClearResponse()
      {
         _httpContext.Response.Clear();
      }
   }
}
