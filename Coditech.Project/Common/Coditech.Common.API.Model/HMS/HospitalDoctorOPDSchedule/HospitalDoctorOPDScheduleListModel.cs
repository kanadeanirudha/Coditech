namespace Coditech.Common.API.Model
{
    public class HospitalDoctorOPDScheduleListModel : BaseListModel
    {
        public List<HospitalDoctorOPDScheduleModel> HospitalDoctorList { get; set; }
        public HospitalDoctorOPDScheduleListModel()
        {
            HospitalDoctorList = new List<HospitalDoctorOPDScheduleModel>();
        }
    }
}
