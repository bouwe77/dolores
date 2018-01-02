﻿using Dolores;
using Dolores.Http;
using Dolores.Responses;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
   internal class TestHandler : DoloresHandler
   {
      public Response Home()
      {
         var response = new Response(HttpStatusCode.Ok);
         response.MessageBody = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
         response.SetContentTypeHeader("text/plain");

         return response;
      }

      public Task<Response> ThrowsException()
      {
         throw new Exception("This Exception is thrown by the TestHandler to verify a 500 Internal Server Error response is returned");
      }

      public Task<Response> ThrowsNotImplementedException()
      {
         throw new NotImplementedException("This NotImplementedException is thrown by the TestHandler to verify a 501 Not Implemented response is returned");
      }

      public Response Dummy()
      {
         return new Response(HttpStatusCode.Ok);
      }
   }
}