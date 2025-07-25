using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseJoiningCodeModel : BaseModel
    {
        public string CentreCode { get; set; }
        public string JoiningCode { get; set; }
        public int Quantity { get; set; }
        public bool IsExpired { get; set; }
        public string CallingCode { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public int TotalJoiningCodeCount { get; set; }
        public int ActiveJoiningCodeCount { get; set; }
        public int ExpireJoiningCodeCount { get; set; }
    }
}
