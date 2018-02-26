using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolores.Routing
{
   [TestClass]
   public class RouteExtensionsTests
   {
      [TestMethod]
      [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "Necessary for unit testing")]
      public void GetUriWithParameterValues_ReturnsNull_WhenRouteIsNull()
      {
         Route route = null;
         var uri = route.GetUriWithParameterValues();

         Assert.IsNull(uri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateEmptyAndNoParameters()
      {
         string uriTemplate = string.Empty;
         var parameterValues = new string[0];
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateEmptyButParametersSupplied()
      {
         string uriTemplate = string.Empty;
         var parameterValues = new[] { "value" };
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateNotEmptyButParametersSupplied()
      {
         string uriTemplate = "resource";
         var parameterValues = new[] { "value" };
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateIsInvalidAndParametersSupplied()
      {
         string uriTemplate = "resource/{id";
         var parameterValues = new[] { "value" };
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateContainsParametersButNoneAreSupplied()
      {
         string uriTemplate = "resource/{id}";
         var parameterValues = new string[0];
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateContainsOneParameterAndOneParameterIsSupplied()
      {
         string uriTemplate = "resource/{id}";
         var parameterValues = new[] { "value" };
         string expectedUri = "resource/value";

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateContainsMultipleParameterButOneParameterIsSupplied()
      {
         string uriTemplate = "resource/{id}/resource2/{id2}";
         var parameterValues = new[] { "value" };
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateContainsOneParameterButMultipleParametersAreSupplied()
      {
         string uriTemplate = "resource/{id}";
         var parameterValues = new[] { "value1", "value2" };
         string expectedUri = uriTemplate;

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      [TestMethod]
      public void GetUriWithParameterValues_ReturnsUriTemplate_WhenUriTemplateContainsMultipleParameterAndMultipleParametersAreSupplied()
      {
         string uriTemplate = "resource/{id}/resource2/{id2}";
         var parameterValues = new[] { "value1", "value2" };
         string expectedUri = "resource/value1/resource2/value2";

         AssertUri(uriTemplate, parameterValues, expectedUri);
      }

      private static void AssertUri(string uriTemplate, string[] parameterValues, string expectedUri)
      {
         var route = new RouteFactory().Create(uriTemplate);

         string uriWithParameterValues = route.GetUriWithParameterValues(parameterValues);

         Assert.AreEqual(expectedUri, uriWithParameterValues);
      }
   }
}
