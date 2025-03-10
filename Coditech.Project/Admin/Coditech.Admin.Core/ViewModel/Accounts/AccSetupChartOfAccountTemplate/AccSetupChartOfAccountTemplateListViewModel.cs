using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupChartOfAccountTemplateListViewModel : BaseViewModel
    {
        public List<AccSetupChartOfAccountTemplateViewModel> AccSetupChartOfAccountTemplateList { get; set; }
        public AccSetupChartOfAccountTemplateListViewModel()
        {
            AccSetupChartOfAccountTemplateList = new List<AccSetupChartOfAccountTemplateViewModel>();
        }
    }
}
