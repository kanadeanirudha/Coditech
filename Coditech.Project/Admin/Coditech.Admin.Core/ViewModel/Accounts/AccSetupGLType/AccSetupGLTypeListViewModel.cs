using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccSetupGLTypeListViewModel : BaseViewModel
    {
        public List<AccSetupGLTypeViewModel> AccSetupGLTypeList { get; set; }
        public AccSetupGLTypeListViewModel()
        {
            AccSetupGLTypeList = new List<AccSetupGLTypeViewModel>();
        }
    }
}
