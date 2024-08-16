namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestGroupListModel : BaseListModel
    {
        public List<HospitalPathologyTestGroupModel> HospitalPathologyTestGroupList { get; set; }
        public HospitalPathologyTestGroupListModel()
        {
            HospitalPathologyTestGroupList = new List<HospitalPathologyTestGroupModel>();
        }
    }
}
