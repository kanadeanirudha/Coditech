namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestListModel : BaseListModel
    {
        public List<HospitalPathologyTestModel> HospitalPathologyTestList { get; set; }
        public HospitalPathologyTestListModel()
        {
            HospitalPathologyTestList = new List<HospitalPathologyTestModel>();
        }
    }
}
