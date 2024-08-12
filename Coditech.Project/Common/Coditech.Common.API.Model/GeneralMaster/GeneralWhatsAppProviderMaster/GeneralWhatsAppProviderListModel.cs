namespace Coditech.Common.API.Model
{
    public  class GeneralWhatsAppProviderListModel : BaseListModel
    {
        public List<GeneralWhatsAppProviderModel> GeneralWhatsAppProviderList { get; set; }
        public GeneralWhatsAppProviderListModel() 
        {
            GeneralWhatsAppProviderList = new List<GeneralWhatsAppProviderModel>();
        }
    }
}
