using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationModel : BaseModel
    {
        public OrganisationModel()
        {
        }
        public byte OrganisationMasterId { get; set; }

        [MaxLength(120)]
        public string EstablishmentCode { get; set; }

        [MaxLength(120)]
        [Required]
        public string OrganisationName { get; set; }

        [Required]
        public System.DateTime FoundationDatetime { get; set; }

        [MaxLength(60)]
        [Required]
        public string FounderMember { get; set; }

        [MaxLength(200)]
        [Required]
        public string Address1 { get; set; }

        [Required]
        public int GeneralCityMasterId { get; set; }

        [MaxLength(15)]
        [Required]
        public string Pincode { get; set; }

        [MaxLength(60)]
        [Required]
        public string EmailId { get; set; }

        [MaxLength(60)]
        public string Url { get; set; }

        [MaxLength(4000)]
        public string OfficeComment { get; set; }

        [MaxLength(4000)]
        public string MissionStatement { get; set; }

        [MaxLength(50)]
        [Required]
        public string MobileNumber { get; set; }

        [MaxLength(50)]
        public string FaxNumber { get; set; }

        [MaxLength(50)]
        public string OfficePhone1 { get; set; }

        [MaxLength(50)]
        public string OfficePhone2 { get; set; }

        [MaxLength(35)]
        public string OrganisationCode { get; set; }
    }
}
