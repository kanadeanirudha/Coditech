using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseJoiningCodeViewModel : BaseViewModel
    {
        public string CentreCode { get; set; }
        public string JoiningCode { get; set; }
        [Required]
        [Display(Name = "Joining Code Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Is Expired")]
        public bool IsExpired { get; set; }
    }
}
