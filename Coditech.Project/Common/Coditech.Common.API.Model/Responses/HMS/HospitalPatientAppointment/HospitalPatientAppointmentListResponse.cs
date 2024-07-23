namespace Coditech.Common.API.Model.Response
{
    public class HospitalPatientAppointmentListResponse : BaseListResponse
    {
        public List<HospitalPatientAppointmentModel> HospitalPatientAppointmentList { get; set; }
        public int MedicalSpecilizationEnumId { get; set; }
        //public string MedicalSpecilization { get; set; }
        public string SelectedCentreCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
