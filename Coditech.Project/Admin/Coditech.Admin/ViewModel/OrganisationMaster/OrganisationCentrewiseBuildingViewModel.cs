﻿using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingViewModel : BaseViewModel
    {
        public short OrganisationCentrewiseBuildingMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
        [Required]
        [Display(Name = "Build Name")]
        public string BuildName { get; set; }
        [Required]
        public short Area { get; set; }        
    }
}
