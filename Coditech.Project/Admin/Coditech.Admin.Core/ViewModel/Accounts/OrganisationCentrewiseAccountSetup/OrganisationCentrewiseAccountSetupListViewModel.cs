using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseAccountSetupListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseAccountSetupViewModel> OrganisationCentrewiseAccountSetupList { get; set; }
        public OrganisationCentrewiseAccountSetupListViewModel()
        {
            OrganisationCentrewiseAccountSetupList = new List<OrganisationCentrewiseAccountSetupViewModel>();
        }
    }
}
