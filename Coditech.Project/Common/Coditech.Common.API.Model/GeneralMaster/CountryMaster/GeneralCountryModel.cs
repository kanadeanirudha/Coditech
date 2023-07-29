using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralCountryModel : BaseModel
    {
        public GeneralCountryModel()
        {

        }
        public short GeneralCountryMasterId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool DefaultFlag { get; set; }
        public bool IsUserDefined { get; set; } = false;
        public short SeqNo { get; set; }
    }
}
