using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentreListViewModel : BaseViewModel
    {
        public List<OrganisationCentreViewModel> OrganisationCentreList { get; set; }

        public OrganisationCentreListViewModel()
        {
            OrganisationCentreList = new List<OrganisationCentreViewModel>();
        }
    }
}
