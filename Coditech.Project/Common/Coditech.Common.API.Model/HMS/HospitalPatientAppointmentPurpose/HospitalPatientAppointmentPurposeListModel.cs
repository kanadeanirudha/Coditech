namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentPurposeListModel : BaseListModel
    {
        public List<HospitalPatientAppointmentPurposeModel> HospitalPatientAppointmentPurposeList { get; set; }
        public HospitalPatientAppointmentPurposeListModel()
        {
            HospitalPatientAppointmentPurposeList = new List<HospitalPatientAppointmentPurposeModel>();
        }
    }
}