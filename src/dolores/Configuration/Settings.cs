using System;
using System.Collections.Generic;

namespace Dolores.Configuration
{
   public class Settings
   {
      public string ErrorDetailsInResponses { get; set; }

      public ErrorResponseDetails ErrorDetailsInResponsesEnum
      {
         get
         {
            if (Enum.TryParse(ErrorDetailsInResponses, out ErrorResponseDetails errorResponseDetails)
               && Enum.IsDefined(typeof(ErrorResponseDetails), ErrorDetailsInResponses) 
                | errorResponseDetails.ToString().Contains(","))
            {
               return errorResponseDetails;
            }

            return errorResponseDetails;
         }
      }

      public List<Route> Routes { get; set; }
   }
}
