using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dolores.Configuration
{
   public class MyTEMPRoute
   {
      public string Identifier { get; set; }
      public string UriTemplate { get; set; }

      public MyTEMPHttpMethodImplementation Get { get; set; }
      public MyTEMPHttpMethodImplementation Post { get; set; }
      public MyTEMPHttpMethodImplementation Put { get; set; }
      public MyTEMPHttpMethodImplementation Delete { get; set; }
      public MyTEMPHttpMethodImplementation Patch { get; set; }
      public MyTEMPHttpMethodImplementation Head { get; set; }
      public MyTEMPHttpMethodImplementation Options { get; set; }

      [JsonIgnore]
      public List<MyTEMPHttpMethodImplementation> Implementations { get; set; }
   }
}
