namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestPricesListModel : BaseListModel
    {
        public List<HospitalPathologyTestPricesModel> HospitalPathologyTestPricesList { get; set; }
        public HospitalPathologyTestPricesListModel()
        {
            HospitalPathologyTestPricesList = new List<HospitalPathologyTestPricesModel>();
        }
    }
}
