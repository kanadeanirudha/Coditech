using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class GeneralSmsProviderListViewModel : BaseViewModel
    {
        public List<GeneralSmsProviderViewModel> GeneralSmsProviderList { get; set; }  
        public GeneralSmsProviderListViewModel()
        {
            GeneralSmsProviderList= new List<GeneralSmsProviderViewModel>();    
        }
    }
}
