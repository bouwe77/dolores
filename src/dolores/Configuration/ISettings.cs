//using System;
//using System.Collections.Generic;
//using Dolores.Requests;
//using Dolores.Responses;
//using Dolores.Routing;
//using Newtonsoft.Json;

//namespace Dolores.Configuration
//{
//   internal interface ISettings
//   {
//      ErrorResponseDetails ErrorDetailsInResponses { get; set; }
//      JsonSerializerSettings JsonSerializerSettings { get; set; }
//      List<Action<Request, Response>> OnBeforeSendResponse { get; set; }

//      Route AddRoute(string routeIdentifier, string uriTemplate);
//      Dictionary<string, Route> GetAllRoutes();
//      Route GetRouteByIdentifier(string routeIdentifier);
//   }
//}