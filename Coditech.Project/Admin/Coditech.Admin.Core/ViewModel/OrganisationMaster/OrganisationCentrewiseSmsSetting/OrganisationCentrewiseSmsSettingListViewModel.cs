using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseSmsSettingListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseSmsSettingViewModel> OrganisationCentrewiseSmsSettingList { get; set; }

        public OrganisationCentrewiseSmsSettingListViewModel()
        {
            OrganisationCentrewiseSmsSettingList = new List<OrganisationCentrewiseSmsSettingViewModel>();
        }
    }
}