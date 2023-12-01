using Coditech.Admin.ViewModel;
using Coditech.Common.Helper;
namespace Coditech.ViewModel
{
    public class OrganisationCentrewiseGSTCredentialListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseGSTCredentialViewModel> OrganisationCentrewiseGSTCredentialList { get; set; }

        public OrganisationCentrewiseGSTCredentialListViewModel()
        {
            OrganisationCentrewiseGSTCredentialList = new List<OrganisationCentrewiseGSTCredentialViewModel>();
        }
    }
}