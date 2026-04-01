using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralSchoolViewModel : BaseViewModel
    {
        public short GeneralSchoolMasterId { get; set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "School Code")]
        public string SchoolCode { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public short GeneralCountryMasterId { get; set; }
        [Required]
        [Display(Name = "State")]
        public short GeneralRegionMasterId { get; set; }
        [Required]
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
    }
}
