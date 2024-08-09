using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel

{
    public class GeneralWhatsAppProviderListViewModel : BaseViewModel
    {
        public List<GeneralWhatsAppProviderViewModel> GeneralWhatsAppProviderList { get; set; } 
        public GeneralWhatsAppProviderListViewModel()
        {
            GeneralWhatsAppProviderList = new List<GeneralWhatsAppProviderViewModel>();
        }

    }
}
