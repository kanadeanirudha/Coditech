namespace Coditech.Common.API.Model
{
    public class GeneralPersonAddressListModel : BaseModel
    {
        public List<GeneralPersonAddressModel> PersonAddressList { get; set; }
        public GeneralPersonAddressListModel()
        {
            PersonAddressList = new List<GeneralPersonAddressModel>();
        }
    }
}
