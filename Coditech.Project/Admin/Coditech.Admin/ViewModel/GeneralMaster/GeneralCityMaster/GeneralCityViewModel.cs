﻿using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCityViewModel : BaseViewModel
    {
        [Required]
        public int GeneralCityMasterId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }

        [Required]
        [Display(Name = "Region")]
        public short GeneralRegionMasterId { get; set; }
        public string RegionName { get; set; }

        [Display(Name = "Country")]
        [Required]
        public string GeneralCountryMasterId { get; set; }
    }
}
