using System;
using System.Collections.Generic;

namespace Dolores.Configuration
{
   public class MyTEMPSettings
   {
      public MyTEMPSettings()
      {
      }

      public string ErrorDetailsInResponses { get; set; }

      public ErrorResponseDetails ErrorDetailsInResponsesEnum
      {
         get
         {
            ErrorResponseDetails errorResponseDetails = ErrorResponseDetails.None;

            if (Enum.TryParse(ErrorDetailsInResponses, out errorResponseDetails)
               && Enum.IsDefined(typeof(ErrorResponseDetails), ErrorDetailsInResponses) | errorResponseDetails.ToString().Contains(","))
            {
               return errorResponseDetails;
            }

            return errorResponseDetails;
         }
      }

      public List<MyTEMPRoute> Routes { get; set; }
   }
}
