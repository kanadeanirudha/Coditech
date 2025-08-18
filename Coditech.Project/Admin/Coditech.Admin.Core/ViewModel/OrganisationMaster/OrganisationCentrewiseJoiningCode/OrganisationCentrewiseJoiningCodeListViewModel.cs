using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseJoiningCodeListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseJoiningCodeViewModel> OrganisationCentrewiseJoiningCodeList { get; set; }
        public OrganisationCentrewiseJoiningCodeListViewModel()
        {
            OrganisationCentrewiseJoiningCodeList = new List<OrganisationCentrewiseJoiningCodeViewModel>();
        }

        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public int JoiningCodeTypeEnumId { get; set; }
        public string SelectedParameter1 { get; set; }

    }
}
