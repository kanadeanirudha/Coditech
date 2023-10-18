using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationMasterViewModel : BaseViewModel
    {
        public byte OrganisationMasterId { get; set; }

        [Display(Name = "Establishment Code")]
        public string EstablishmentCode { get; set; }

        [Required]
        [Display(Name = "Organisation Name")]
        public string OrganisationName { get; set; }

        [Display(Name = "Foundation Date time")]
        public System.DateTime FoundationDatetime { get; set; }

        [Required]
        [Display(Name = "Founder Member")]
        public string FounderMember { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Required]
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }

        [Required]
        [Display(Name = "Pin code")]
        public string Pincode { get; set; }

        [Required]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        public string Url { get; set; }

        [Display(Name = "Office Comment")]
        public string OfficeComment { get; set; }

        [Display(Name = "Mission Statement")]
        public string MissionStatement { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Display(Name = "Office Phone1")]
        public string OfficePhone1 { get; set; }

        [Display(Name = "Office Phone2")]
        public string OfficePhone2 { get; set; }

        [Required]
        [Display(Name = "Organisation Code")]
        public string OrganisationCode { get; set; }

        [Display(Name = "PF Number")]
        public string PFNumber { get; set; }

        [Display(Name = "ESIC Number")]
        public string ESICNumber { get; set; }
    }
}
