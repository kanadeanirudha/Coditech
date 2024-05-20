namespace Coditech.Common.API.Model
{
    public class GeneralPersonAddressListModel : BaseModel
    {
        public GeneralPersonAddressListModel()
        {
            PersonAddressList = new List<GeneralPersonAddressModel>();
        }
        public List<GeneralPersonAddressModel> PersonAddressList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
