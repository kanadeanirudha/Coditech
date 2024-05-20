namespace Coditech.Common.API.Model
{
    public class HospitalDoctorOPDScheduleListModel : BaseListModel
    {
        public List<HospitalDoctorOPDScheduleModel> HospitalDoctorOPDScheduleList { get; set; }
        public HospitalDoctorOPDScheduleListModel()
        {
            HospitalDoctorOPDScheduleList = new List<HospitalDoctorOPDScheduleModel>();
        }
    }
}
