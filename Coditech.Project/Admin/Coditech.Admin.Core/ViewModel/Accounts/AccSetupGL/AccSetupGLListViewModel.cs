using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccSetupGLListViewModel : BaseViewModel
    {
        public List<AccSetupGLViewModel> AccSetupGLList { get; set; }
        public AccSetupGLListViewModel()
        {
            AccSetupGLList = new List<AccSetupGLViewModel>();
        }
    }
}
