
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseUserNameRegistrationListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseUserNameRegistrationViewModel> OrganisationCentrewiseUserNameRegistrationList { get; set; }

        public OrganisationCentrewiseUserNameRegistrationListViewModel()
        {
            OrganisationCentrewiseUserNameRegistrationList = new List<OrganisationCentrewiseUserNameRegistrationViewModel>();
        }
    }
}
