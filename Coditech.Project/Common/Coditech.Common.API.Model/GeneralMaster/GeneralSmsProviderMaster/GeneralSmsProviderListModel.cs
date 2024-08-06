namespace Coditech.Common.API.Model
{
    public class GeneralSmsProviderListModel : BaseListModel
    {
        public List<GeneralSmsProviderModel> GeneralSmsProviderList { get; set; }  
        public GeneralSmsProviderListModel() 
        {
            GeneralSmsProviderList= new List<GeneralSmsProviderModel>();
        }
    }
}
    
