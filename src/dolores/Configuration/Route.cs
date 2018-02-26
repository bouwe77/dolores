using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dolores.Configuration
{
   public class Route
   {
      public string Identifier { get; set; }
      public string UriTemplate { get; set; }

      public HttpMethodImplementation Get { get; set; }
      public HttpMethodImplementation Post { get; set; }
      public HttpMethodImplementation Put { get; set; }
      public HttpMethodImplementation Delete { get; set; }
      public HttpMethodImplementation Patch { get; set; }
      public HttpMethodImplementation Head { get; set; }
      public HttpMethodImplementation Options { get; set; }

      [JsonIgnore]
      public List<HttpMethodImplementation> Implementations { get; set; }
   }
}
