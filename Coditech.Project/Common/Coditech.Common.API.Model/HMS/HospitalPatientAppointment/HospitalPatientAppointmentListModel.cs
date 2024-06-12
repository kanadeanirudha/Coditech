namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentListModel : BaseListModel
    {
        public List<HospitalPatientAppointmentModel> HospitalPatientAppointmentList { get; set; }
        public HospitalPatientAppointmentListModel()
        {
            HospitalPatientAppointmentList = new List<HospitalPatientAppointmentModel>();
        }
    }
}
