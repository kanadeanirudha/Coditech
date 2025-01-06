using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseWhatsAppSettingListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseWhatsAppSettingViewModel> OrganisationCentrewiseWhatsAppSettingList { get; set; }

        public OrganisationCentrewiseWhatsAppSettingListViewModel()
        {
            OrganisationCentrewiseWhatsAppSettingList = new List<OrganisationCentrewiseWhatsAppSettingViewModel>();
        }
    }
}
