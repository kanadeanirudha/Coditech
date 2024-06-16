namespace Coditech.Common.API.Model
{
    public class HospitalPatientTypeListModel : BaseListModel
    {
        public List<HospitalPatientTypeModel> HospitalPatientTypeList { get; set; }
        public HospitalPatientTypeListModel()
        {
            HospitalPatientTypeList = new List<HospitalPatientTypeModel>();
        }
    }
}
