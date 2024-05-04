namespace Coditech.Common.API.Model
{
    public class HospitalDoctorLeaveScheduleListModel : BaseListModel
    {
        public List<HospitalDoctorLeaveScheduleModel> HospitalDoctorLeaveScheduleList { get; set; }
        public HospitalDoctorLeaveScheduleListModel()
        {
            HospitalDoctorLeaveScheduleList = new List<HospitalDoctorLeaveScheduleModel>();
        }
    }
}
