using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEmailTemplateListViewModel : BaseViewModel
    {
        public List<GeneralEmailTemplateViewModel> GeneralEmailTemplateList { get; set; }
        public GeneralEmailTemplateListViewModel()
        {
            GeneralEmailTemplateList = new List<GeneralEmailTemplateViewModel>();
        }
    }
}
