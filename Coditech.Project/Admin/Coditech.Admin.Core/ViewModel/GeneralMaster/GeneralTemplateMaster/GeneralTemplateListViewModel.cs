using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTemplateListViewModel : BaseViewModel
    {
        public List<GeneralTemplateViewModel> GeneralTemplateList { get; set; }
        public GeneralTemplateListViewModel()
        {
            GeneralTemplateList = new List<GeneralTemplateViewModel>();
        }
    }
}
