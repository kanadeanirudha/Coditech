using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrePrintingListViewModel : BaseViewModel
    {
        public List<OrganisationCentrePrintingFormatViewModel> OrganisationCentrePrintingFormatList { get; set; }

        public OrganisationCentrePrintingListViewModel()
        {
            OrganisationCentrePrintingFormatList = new List<OrganisationCentrePrintingFormatViewModel>();
        }
    }
}