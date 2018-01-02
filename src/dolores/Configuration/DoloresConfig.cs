//using System;
//using Dolores.Diagnostics;
//using Dolores.Requests;
//using Dolores.Responses;
//using Dolores.Routing;
//using Newtonsoft.Json;

//namespace Dolores.Configuration
//{
//   /// <summary>
//   /// This class enables you to configure Dolores.
//   /// It is typically used in the Application_Start of the Global.asax.cs in your application.
//   /// </summary>
//   public class DoloresConfig
//   {
//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="routeIdentifier"></param>
//      /// <param name="uriTemplate"></param>
//      /// <returns></returns>
//      public static RouteSettings Route(string routeIdentifier, string uriTemplate)
//      {
//         var addedRoute = Settings.Instance.AddRoute(routeIdentifier, uriTemplate);
//         return new RouteSettings(addedRoute);
//      }

//      /// <summary>
//      /// Sets a value indicating which error details must be shown in HTTP error responses.
//      /// </summary>
//      public static ErrorResponseDetails ErrorDetailsInResponses
//      {
//         set { Settings.Instance.ErrorDetailsInResponses = value; }
//      }

//      /// <summary>
//      /// Sets the factory method for creating an instance of your logger so you can see Dolores logging in your application's logfile.
//      /// </summary>
//      /// <value>The factory method.</value>
//      public static Func<ILogger> CreateLogger
//      {
//         set { Logger.Instance.CreateLogger = value; }
//      }

//      /// <summary>
//      /// Sets the <see cref="JsonSerializerSettings"/> that will be used by Dolores when serializing JSON.
//      /// </summary>
//      public static JsonSerializerSettings JsonSerializerSettings
//      {
//         set
//         {
//            if (value != null)
//            {
//               Settings.Instance.JsonSerializerSettings = value;
//            }
//         }
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="action"></param>
//      public static void OnSendResponse(Action<Request, Response> action)
//      {
//         Settings.Instance.OnBeforeSendResponse.Add(action);
//      }

//      /// <summary>
//      /// Clears all routes, for unit testing only.
//      /// </summary>
//      internal static void Clear()
//      {
//         Settings.Instance.ClearRoutes();
//      }
//   }
//}
