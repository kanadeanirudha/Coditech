using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralLeadGenerationListViewModel : BaseViewModel
    {
        public List<GeneralLeadGenerationViewModel> GeneralLeadGenerationList { get; set; }
        public GeneralLeadGenerationListViewModel()
        {
            GeneralLeadGenerationList = new List<GeneralLeadGenerationViewModel>();
        }
        public string SelectedCentreCode { get; set; }
    }
}
