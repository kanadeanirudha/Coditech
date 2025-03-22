using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseAccountSetupViewModel : BaseViewModel
    {
        public int OrganisationCentrewiseAccountSetupId { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public short GeneralCurrencyMasterId { get; set; }
    }
}
