using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Coditech.Common.API.Model
{
    public class GeneralSmsProviderModel : BaseModel
    {
      public GeneralSmsProviderModel() 
        { 
        
   
        }
       
        public short GeneralSmsProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderCode { get; set; }  
        public bool IsActive { get; set; }
    }
}
