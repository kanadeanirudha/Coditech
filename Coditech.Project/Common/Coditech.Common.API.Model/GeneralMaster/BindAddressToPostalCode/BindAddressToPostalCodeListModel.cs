namespace Coditech.Common.API.Model
{
    public class BindAddressToPostalCodeListModel : BaseListModel
    {
        public List<BindAddressToPostalCodeModel> BindAddressToPostalCodeList { get; set; }
        public BindAddressToPostalCodeListModel()
        {
            BindAddressToPostalCodeList = new List<BindAddressToPostalCodeModel>();
        }
        public string Message { get; set; }
        public string Status { get; set; }


    }
}
