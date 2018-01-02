using Dolores.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolores.Helpers
{
   [TestClass]
   public class HttpStatusCodeExtensionsTests
   {
      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeOk()
      {
         var httpStatusCode = HttpStatusCode.Ok;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("OK", reasonPhrase);
      }

      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeIsOneWord()
      {
         var httpStatusCode = HttpStatusCode.Continue;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("Continue", reasonPhrase);
      }

      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeIsMultipleWords()
      {
         var httpStatusCode = HttpStatusCode.InternalServerError;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("Internal Server Error", reasonPhrase);
      }

      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeHasHttpAcronym()
      {
         var httpStatusCode = HttpStatusCode.HttpVersionNotSupported;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("HTTP Version Not Supported", reasonPhrase);
      }

      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeHasNonAuthoritative()
      {
         var httpStatusCode = HttpStatusCode.NonAuthoritativeInformation;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("Non-Authoritative Information", reasonPhrase);
      }

      [TestMethod]
      public void GetReasonPhrase_ReturnsExpectedValue_WhenStatusCodeHasRequestUri()
      {
         var httpStatusCode = HttpStatusCode.RequestUriTooLong;

         string reasonPhrase = httpStatusCode.GetReasonPhrase();

         Assert.AreEqual("Request-URI Too Long", reasonPhrase);
      }


   }
}
