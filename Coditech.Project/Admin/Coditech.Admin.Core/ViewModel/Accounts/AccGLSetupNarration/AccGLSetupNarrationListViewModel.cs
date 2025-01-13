using System.ComponentModel.DataAnnotations;
using Coditech.Common.Helper;
using Coditech.Resources;
namespace Coditech.Admin.ViewModel
{
    public class AccGLSetupNarrationListViewModel : BaseViewModel
    {
        public List<AccGLSetupNarrationViewModel> AccGLSetupNarrationList { get; set; }
        public AccGLSetupNarrationListViewModel()
        {
            AccGLSetupNarrationList = new List<AccGLSetupNarrationViewModel>();
        }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; } = null;
    }
}
