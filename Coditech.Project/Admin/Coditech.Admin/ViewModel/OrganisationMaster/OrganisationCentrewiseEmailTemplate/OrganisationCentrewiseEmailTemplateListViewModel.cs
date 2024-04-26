using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseEmailTemplateListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseEmailTemplateViewModel> OrganisationCentrewiseEmailTemplateList { get; set; }

        public OrganisationCentrewiseEmailTemplateListViewModel()
        {
            OrganisationCentrewiseEmailTemplateList = new List<OrganisationCentrewiseEmailTemplateViewModel>();
        }
    }
}