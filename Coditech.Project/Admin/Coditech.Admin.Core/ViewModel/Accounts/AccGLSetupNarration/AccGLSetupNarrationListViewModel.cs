using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccGLSetupNarrationListViewModel : BaseViewModel
    {
        public List<AccGLSetupNarrationViewModel> AccGLSetupNarrationList { get; set; }
        public AccGLSetupNarrationListViewModel()
        {
            AccGLSetupNarrationList = new List<AccGLSetupNarrationViewModel>();
        }
    }
}
