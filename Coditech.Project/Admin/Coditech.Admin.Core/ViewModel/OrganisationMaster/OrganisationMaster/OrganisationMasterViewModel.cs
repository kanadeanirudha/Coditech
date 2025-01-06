using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationMasterViewModel : BaseViewModel
    {
        public byte OrganisationMasterId { get; set; }

        [MaxLength(120)]
        [Display(Name = "Establishment Code")]
        public string EstablishmentCode { get; set; }

        [MaxLength(120)]
        [Required]
        [Display(Name = "Organisation Name")]
        public string OrganisationName { get; set; }

        [Required]
        [Display(Name = "Foundation Date time")]
        public System.DateTime FoundationDatetime { get; set; }

        [MaxLength(60)]
        [Required]
        [Display(Name = "Founder Member")]
        public string FounderMember { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Required]
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Pin code")]
        public string Pincode { get; set; }

        [MaxLength(60)]
        [Required]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [MaxLength(60)]
        public string Url { get; set; }

        [MaxLength(4000)]
        [Display(Name = "Office Comment")]
        public string OfficeComment { get; set; }

        [MaxLength(4000)]
        [Display(Name = "Mission Statement")]
        public string MissionStatement { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Office Phone1")]
        public string OfficePhone1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Office Phone2")]
        public string OfficePhone2 { get; set; }

        [MaxLength(35)]
        [Display(Name = "Organisation Code")]
        public string OrganisationCode { get; set; }
    }
}
