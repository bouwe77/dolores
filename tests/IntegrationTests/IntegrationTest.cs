using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using TestApp;
using System.Threading.Tasks;
using System.Net;
using System;

//TODO Code checken voor nog meer testcases

namespace IntegrationTests
{
   /// <summary>
   ///  The application under test is the "TestApp" that can be found in the examples solution folder.
   /// </summary>
   [TestClass]
   public class IntegrationTest
   {
      private readonly HttpClient _client;

      public IntegrationTest()
      {
         var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
         _client = server.CreateClient();
      }

      /// <summary>
      /// The route for this URI is configured with a method that returns a successful response.
      /// </summary>
      [TestMethod]
      public async Task OkResponse_WhenHomeRouteRequested()
      {
         var response = await SendAndAssertGETRequest("", HttpStatusCode.OK);
         var content = await response.Content.ReadAsStringAsync();
         Assert.AreEqual("Hello World", content);
      }

      /// <summary>
      /// The route for this URI is configured with a class method that throws an <see cref="NotImplementedException"/>.
      /// The expected HTTP status code when this exception occurs is a 501 Not Implemented.
      /// </summary>
      [TestMethod]
      public async Task NotImplementedResponse_WhenCodeThrowsNotImplementedException()
      {
         await SendAndAssertGETRequest("/throwsnotimplementedexception", HttpStatusCode.NotImplemented);
      }

      /// <summary>
      /// The route for this URI is configured with a class method that throws an <see cref="Exception"/>.
      /// The expected HTTP status code when this exception occurs is a 500 Internal Server Error.
      /// </summary>
      [TestMethod]
      public async Task InternalServerResponse_WhenCodeThrowsException()
      {
         await SendAndAssertGETRequest("/throwsexception", HttpStatusCode.InternalServerError);
      }

      /// <summary>
      /// The route for this URI is configured with a type (class) that does not exist.
      /// The expected HTTP status code when requesting such a config error is a 501 Not Implemented.
      /// </summary>
      [TestMethod]
      public async Task NotImplementedResponse_WhenConfiguredClassDoesNotExist()
      {
         await SendAndAssertGETRequest("/classdoesnotexist", HttpStatusCode.NotImplemented);
      }

      /// <summary>
      /// The route for this URI is configured with a class method name that does not exist.
      /// The expected HTTP status code when requesting such a config error is a 501 Not Implemented.
      /// </summary>
      [TestMethod]
      public async Task NotImplementedResponse_WhenConfiguredMethodDoesNotExist()
      {
         await SendAndAssertGETRequest("/methodnamedoesnotexistonclass", HttpStatusCode.NotImplemented);
      }

      /// <summary>
      /// The "GET only" route only supports a GET request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task GetOnlyRouteReturnsOk_WhenRequestIsGet()
      {
         await SendAndAssertGETRequest("/getonly", HttpStatusCode.OK);
         await SendAndAssertGETRequest("/gEtOnLy", HttpStatusCode.OK);
      }

      /// <summary>
      /// The "POST only" route only supports a POST request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task PostOnlyRouteReturnsOk_WhenRequestIsPost()
      {
         await SendAndAssertPOSTRequest("/postonly", null, HttpStatusCode.OK);
         await SendAndAssertPOSTRequest("/pOstOnLy", null, HttpStatusCode.OK);
      }

      /// <summary>
      /// The "PUT only" route only supports a PUT request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task PutOnlyRouteReturnsOk_WhenRequestIsPut()
      {
         await SendAndAssertPUTRequest("/putonly", null, HttpStatusCode.OK);
         await SendAndAssertPUTRequest("/pUtOnLy", null, HttpStatusCode.OK);
      }

      /// <summary>
      /// The "DELETE only" route only supports a DELETE request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task DeleteOnlyRouteReturnsOk_WhenRequestIsDelete()
      {
         await SendAndAssertDELETERequest("/deleteonly", HttpStatusCode.OK);
         await SendAndAssertDELETERequest("/dElEtEoNlY", HttpStatusCode.OK);
      }

      /// <summary>
      /// The "PATCH only" route only supports a PATCH request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task PatchOnlyRouteReturnsOk_WhenRequestIsPatch()
      {
         await SendAndAssertPATCHRequest("/patchonly", null, HttpStatusCode.OK);
         await SendAndAssertPATCHRequest("/pAtChOnly", null, HttpStatusCode.OK);
      }

      /// <summary>
      /// The "HEAD only" route only supports a HEAD request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task HeadOnlyRouteReturnsOk_WhenRequestIsHead()
      {
         await SendAndAssertHEADRequest("/headonly", HttpStatusCode.OK);
         await SendAndAssertHEADRequest("/hEaDoNlY", HttpStatusCode.OK);
      }

      /// <summary>
      /// The "OPTIONS only" route only supports a OPTIONS request. This method verifies the route returns an OK response.
      /// </summary>
      [TestMethod]
      public async Task OptionsOnlyRouteReturnsOk_WhenRequestIsOptions()
      {
         await SendAndAssertOPTIONSRequest("/optionsonly", HttpStatusCode.OK);
         await SendAndAssertOPTIONSRequest("/oPtIoNsOnLy", HttpStatusCode.OK);
      }

      /// <summary>
      /// The "GET only" route only supports a GET request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task GetOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertPOSTRequest("/getonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/getonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/getonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/getonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/getonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/getonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "POST only" route only supports a POST request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task PostOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/postonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/postonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/postonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/postonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/postonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/postonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "PUT only" route only supports a PUT request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task PutOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/putonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPOSTRequest("/putonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/putonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/putonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/putonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/putonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "DELETE only" route only supports a DELETE request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task DeleteOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/deleteonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPOSTRequest("/deleteonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/deleteonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/deleteonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/deleteonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/deleteonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "PATCH only" route only supports a PATCH request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task PatchOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/patchonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPOSTRequest("/patchonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/patchonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/patchonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/patchonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/patchonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "HEAD only" route only supports a HEAD request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task HeadOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/headonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPOSTRequest("/headonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/headonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/headonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/headonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertOPTIONSRequest("/headonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// The "OPTIONS only" route only supports a OPTIONS request. This method verifies any other HTTP method returns a Method Not Allowed response.
      /// </summary>
      [TestMethod]
      public async Task OptionsOnlyRouteReturnsMethodNotAllowed_WhenRequestIsAnyOtherMethod()
      {
         await SendAndAssertGETRequest("/optionsonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPOSTRequest("/optionsonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPUTRequest("/optionsonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertDELETERequest("/optionsonly", HttpStatusCode.MethodNotAllowed);
         await SendAndAssertPATCHRequest("/optionsonly", null, HttpStatusCode.MethodNotAllowed);
         await SendAndAssertHEADRequest("/optionsonly", HttpStatusCode.MethodNotAllowed);
      }

      /// <summary>
      /// A GET request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task GetReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertGETRequest("/abcdef", HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A POST request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task PostReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertPOSTRequest("/abcdef", null, HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A PUT request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task PutReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertPUTRequest("/abcdef", null, HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A DELETE request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task DeleteReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertDELETERequest("/abcdef", HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A PATCH request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task PatchReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertPATCHRequest("/abcdef", null, HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A HEAD request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task HeadReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertHEADRequest("/abcdef", HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A OPTIONS request to an URI that is not configured as route must return a 404 Not Found.
      /// </summary>
      [TestMethod]
      public async Task OptionsReturnsNotFound_WhenUriNotConfigured()
      {
         await SendAndAssertOPTIONSRequest("/abcdef", HttpStatusCode.NotFound);
      }

      /// <summary>
      /// A request with an unknown HTTP method must return a 501 Method Not Allowed.
      /// An unknown method is anything other than GET, POST, PUT, DELETE, PATCH, HEAD or OPTIONS.
      /// </summary>
      [TestMethod]
      public async Task ResponseIsMethodNotAllowed_WhenRequestingWithUnknownHttpMethod()
      {
         HttpRequestMessage request = new HttpRequestMessage
         {
            Method = new HttpMethod("UNKNOWN"),
            RequestUri = new Uri("", UriKind.Relative)
         };

         var response = await _client.SendAsync(request);

         Assert.AreEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
      }

      private async Task<HttpResponseMessage> SendAndAssertGETRequest(string relativeUri, HttpStatusCode expectedStatusCode)
      {
         var response = await _client.GetAsync(relativeUri);
         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertPOSTRequest(string relativeUri, HttpContent content, HttpStatusCode expectedStatusCode)
      {
         var response = await _client.PostAsync(relativeUri, content);
         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertPUTRequest(string relativeUri, HttpContent content, HttpStatusCode expectedStatusCode)
      {
         var response = await _client.PutAsync(relativeUri, content);
         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertDELETERequest(string relativeUri, HttpStatusCode expectedStatusCode)
      {
         var response = await _client.DeleteAsync(relativeUri);
         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertPATCHRequest(string relativeUri, HttpContent content, HttpStatusCode expectedStatusCode)
      {
         HttpRequestMessage request = new HttpRequestMessage
         {
            Method = new HttpMethod("PATCH"),
            RequestUri = new Uri(relativeUri, UriKind.Relative)
         };

         var response = await _client.SendAsync(request);

         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertHEADRequest(string relativeUri, HttpStatusCode expectedStatusCode)
      {
         HttpRequestMessage request = new HttpRequestMessage
         {
            Method = HttpMethod.Head,
            RequestUri = new Uri(relativeUri, UriKind.Relative)
         };

         var response = await _client.SendAsync(request);

         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }

      private async Task<HttpResponseMessage> SendAndAssertOPTIONSRequest(string relativeUri, HttpStatusCode expectedStatusCode)
      {
         HttpRequestMessage request = new HttpRequestMessage
         {
            Method = HttpMethod.Options,
            RequestUri = new Uri(relativeUri, UriKind.Relative)
         };

         var response = await _client.SendAsync(request);

         Assert.AreEqual(expectedStatusCode, response.StatusCode);
         return response;
      }
   }
}
