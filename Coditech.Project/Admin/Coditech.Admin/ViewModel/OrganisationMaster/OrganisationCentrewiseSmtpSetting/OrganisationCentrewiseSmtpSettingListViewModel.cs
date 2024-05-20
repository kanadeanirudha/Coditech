using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseSmtpSettingListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseSmtpSettingViewModel> OrganisationCentrewiseSmtpSettingList { get; set; }

        public OrganisationCentrewiseSmtpSettingListViewModel()
        {
            OrganisationCentrewiseSmtpSettingList = new List<OrganisationCentrewiseSmtpSettingViewModel>();
        }
    }
}