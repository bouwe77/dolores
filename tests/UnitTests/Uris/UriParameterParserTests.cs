using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolores.Uris
{
   [TestClass]
   public class UriParameterParserTests
   {
      [TestMethod]
      public void Parse_ReturnsExpectedValue_WhenBothUrisEmpty()
      {
         var uriParameters = UriParameterParser.Parse(string.Empty, string.Empty);

         Assert.AreEqual(0, uriParameters.Count);
      }

      [TestMethod]
      public void Parse_ReturnsExpectedValue_WhenUriTemplateDoesNotContainDynamicParts()
      {
         var uriParameters = UriParameterParser.Parse("stuff", "stuff");

         Assert.AreEqual(0, uriParameters.Count);
      }

      [TestMethod]
      public void Parse_ReturnsExpectedValue_WhenUriTemplateContainsOneDynamicPart()
      {
         var uriParameters = UriParameterParser.Parse("stuff/{stuffid}", "stuff/1");

         Assert.AreEqual(1, uriParameters.Count);

         var uriParameter = uriParameters.First();
         Assert.AreEqual("stuffid", uriParameter.Key);
         Assert.AreEqual("1", uriParameter.Value);
      }

      [TestMethod]
      public void Parse_ReturnsExpectedValue_WhenUriTemplateContainsMultipleDynamicParts()
      {
         var uriParameters = UriParameterParser.Parse("stuff/{stuffid}/otherstuff/{otherstuffid}", "stuff/1/otherstuff/2");

         Assert.AreEqual(2, uriParameters.Count);

         var uriParameter = uriParameters.First();
         Assert.AreEqual("stuffid", uriParameter.Key);
         Assert.AreEqual("1", uriParameter.Value);

         uriParameter = uriParameters.Last();
         Assert.AreEqual("otherstuffid", uriParameter.Key);
         Assert.AreEqual("2", uriParameter.Value);
      }
   }
}
