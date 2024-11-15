using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingViewModel : BaseViewModel
    {
        public short OrganisationCentrewiseBuildingMasterId { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre")]
        public string CentreCode { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Building Name")]
        public string BuildingName { get; set; }

        [Required]
        [Display(Name = "Area sq.ft")]
        public Nullable<short> Area { get; set; }        
    }
}
