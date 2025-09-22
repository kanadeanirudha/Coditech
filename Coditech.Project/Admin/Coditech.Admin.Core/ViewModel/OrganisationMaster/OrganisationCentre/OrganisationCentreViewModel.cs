using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentreViewModel : BaseViewModel
    {
        public int OrganisationCentreMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }
        [MaxLength(5)]
        public string HoCoRoScFlag { get; set; }
        [Display(Name = "Office Type")]
        public string OfficeType { get; set; }
        public int? HoId { get; set; }
        public int? CoId { get; set; }
        public int? RoId { get; set; }
        [MaxLength(100)]
        [Display(Name = "Centre Specialization")]
        public string CentreSpecialization { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "Address")]
        public string CentreAddress { get; set; }
        [Required]
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Pin code")]
        public string Pincode { get; set; }
        [MaxLength(70)]
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailId { get; set; }
        [MaxLength(30)]
        public string Url { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter valid Mobile Number")]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Phone Number Office")]
        public string PhoneNumberOffice { get; set; }
        [Display(Name = "Establishment Date")]
        public DateTime? CentreEstablishmentDatetime { get; set; }
        [Required]
        [Display(Name = "Organisation Name")]
        public byte OrganisationId { get; set; }
        [Display(Name = "Centre Login Number")]
        public int? CentreLoginNumber { get; set; }
        [MaxLength(50)]
        [Display(Name = "Institute Code")]
        public string InstituteCode { get; set; }
        [MaxLength(32)]
        public string TimeZone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        [Display(Name = "Campus Area")]
        public double? CampusArea { get; set; }
        [MaxLength(25)]
        [Display(Name = "CIN Number")]
        public string CINNumber { get; set; }
        [MaxLength(25)]
        [Display(Name = "GSTIN Number")]
        public string GSTINNumber { get; set; }
        [MaxLength(25)]
        [Display(Name = "Pan Number ")]
        public string PanNumber { get; set; }
        [MaxLength(35)]
        [Display(Name = "PF Number ")]
        public string PFNumber { get; set; }
        [MaxLength(35)]
        [Display(Name = "ESIC Number ")]
        public string ESICNumber { get; set; }
        [MaxLength(35)]
        public string WaterMark { get; set; }
        [Display(Name = "Logo")]
        public long LogoMediaId { get; set; }
        [Display(Name = "Logo Small")]
        public long LogoSmallMediaId { get; set; }
        public string LogoMediaPath { get; set; }
        public string LogoMediaFileName { get; set; }
        public string LogoSmallMediaPath { get; set; }
        public string LogoSmallMediaFileName { get; set; }
        [Display(Name = "Logo Css")]
        public string LogoCss { get; set; }
        [Display(Name = "Small Logo Css")]
        public string SmallLogoCss { get; set; }
    }
}
