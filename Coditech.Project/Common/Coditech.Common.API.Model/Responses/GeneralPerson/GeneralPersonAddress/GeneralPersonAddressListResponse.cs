namespace Coditech.Common.API.Model.Response
{
    public class GeneralPersonAddressListResponse : BaseListResponse
    {
        public List<GeneralPersonAddressModel> GeneralPersonAddressList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
