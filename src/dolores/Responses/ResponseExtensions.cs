using System;
using Dolores.Configuration;
using Dolores.Exceptions;
using Dolores.Helpers;
using Dolores.Http;
using Dolores.Routing;
using Newtonsoft.Json;

namespace Dolores.Responses
{
   /// <summary>
   /// Extension methods for the <see cref="ResponseExtensions"/> class.
   /// </summary>
   public static class ResponseExtensions
   {
      /// <summary>
      /// Serializes the given object to a JSON string and puts it in the body of the response.
      /// Also adds an "application/json" Content-Type header.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <param name="dataToSerialize">The data to serialize.</param>
      /// <exception cref="DoloresSerializationException">Thrown when the serialization fails.</exception>
      public static void Json(this Response response, object dataToSerialize)
      {
         var jsonSerializerSettings = new JsonSerializerSettings
         {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
         };

         response.Json(dataToSerialize, jsonSerializerSettings);
      }

      /// <summary>
      /// Serializes the given object to a JSON string and puts it in the body of the response.
      /// Also adds an "application/json" Content-Type header.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <param name="dataToSerialize">The data to serialize.</param>
      /// <param name="jsonSerializerSettings">The JSON serializer settings.</param>
      /// <exception cref="DoloresSerializationException">Thrown when the serialization fails.</exception>
      public static void Json(this Response response, object dataToSerialize, JsonSerializerSettings jsonSerializerSettings)
      {
         Enforce.ArgumentNotNull(jsonSerializerSettings, nameof(jsonSerializerSettings));

         string json;
         try
         {
            json = JsonConvert.SerializeObject(dataToSerialize, jsonSerializerSettings);
         }
         catch (Exception exception)
         {
            throw new DoloresSerializationException("An error occurred while serializing to a JSON string", exception);
         }

         var jsonStream = json.ToStream();
         response.MessageBody = jsonStream;
         response.SetContentTypeHeader("application/json");
      }

      /// <summary>
      /// Adds the given contentType value as Content-Type header to the response.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <param name="contentTypeHeaderValue">The Content-Type header value.</param>
      public static void SetContentTypeHeader(this Response response, string contentTypeHeaderValue)
      {
         response.SetHeader(HttpResponseHeaderFields.ContentType, contentTypeHeaderValue);
      }

      /// <summary>
      /// Sets the location header by route identifier from the config.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <param name="routeIdentifier">The route identifier as defined in the route config.</param>
      /// <param name="parameterValues">The parameter values.</param>
      //public static void SetLocationHeaderByRouteIdentifier(this Response response, string routeIdentifier, params string[] parameterValues)
      //{
      //   //TODO Deze kan ik nu niet wegmocken. Als dat wel nodig is dan kan deze methode niet meer een extension method blijven...
      //   var route = Settings.Instance.GetRouteByIdentifier(routeIdentifier);
      //   var uri = route.GetUriWithParameterValues(parameterValues);

      //   if (!string.IsNullOrWhiteSpace(uri))
      //   {
      //      response.SetLocationHeader(uri);
      //   }
      //   else
      //   {
      //      Logger.Instance.Warn($"Location header could not be set, because route identifier '{routeIdentifier}' does not exist");
      //   }
      //}

      /// <summary>
      /// Sets the location header on the response.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <param name="locationUri">The location URI.</param>
      public static void SetLocationHeader(this Response response, string locationUri)
      {
         response.SetHeader(HttpResponseHeaderFields.Location, locationUri);
      }
   }
}
