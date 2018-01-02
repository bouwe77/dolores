using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolores.Uris
{
   /// <summary>
   /// Unit tests for the <see cref="UriTemplateToRegexConverter"/> class.
   /// </summary>
   [TestClass]
   public class UriTemplateToRegexConverterTests
   {
      [TestMethod]
      public void Convert_RegexForEmptyValue_WhenUriTemplateIsEmpty()
      {
         string uriTemplate = string.Empty;
         string expectedRegex = "^$";

         AssertConvertedRegex(uriTemplate, expectedRegex);
      }

      [TestMethod]
      public void Convert_RegexForEmptyEmpty_WhenUriTemplateIsSlash()
      {
         string uriTemplate = "/";
         string expectedRegex = "^$";

         AssertConvertedRegex(uriTemplate, expectedRegex);
      }

      [TestMethod]
      public void Convert_RegexIsWord_WhenUriTemplateIsWordWithSlashes()
      {
         string expectedRegex = "^(word)$";

         string uriTemplate = "word";
         AssertConvertedRegex(uriTemplate, expectedRegex, true);
      }

      [TestMethod]
      public void Convert_RegexIsAnyCharacterExceptSlash_WhenUriTemplateIsId()
      {
         string expectedRegex = @"^([^\/]+)$";

         string uriTemplate = "{id}";
         AssertConvertedRegex(uriTemplate, expectedRegex, true);
      }

      [TestMethod]
      public void Convert_WhenUriTemplateContainsId()
      {
         string uriTemplate = "/hoi/{id}/moio";
         string expectedRegex = @"^(hoi)\/([^\/]+)\/(moio)$";

         AssertConvertedRegex(uriTemplate, expectedRegex, true);
      }

      [TestMethod]
      public void Convert_RegexForAnyValue_WhenUriTemplateIsAsterisk()
      {
         string uriTemplate = "*";
         string expectedRegex = @".*?";

         AssertConvertedRegex(uriTemplate, expectedRegex);
      }

      private static void AssertConvertedRegex(string uriTemplate, string expectedRegex, bool addLeadingAndTrailingSlashes = false)
      {
         var uriTemplates = new List<string>
         {
            uriTemplate
         };

         if (addLeadingAndTrailingSlashes)
         {
            uriTemplates.Add($"/{uriTemplate}");
            uriTemplates.Add($"/{uriTemplate}/");
            uriTemplates.Add($"{uriTemplate}/");
         }

         foreach (var template in uriTemplates)
         {
            string actualRegex = UriTemplateToRegexConverter.Convert(template);
            Assert.AreEqual(expectedRegex, actualRegex, $"'{template}' was not converted as expected");
         }
      }
   }
}
