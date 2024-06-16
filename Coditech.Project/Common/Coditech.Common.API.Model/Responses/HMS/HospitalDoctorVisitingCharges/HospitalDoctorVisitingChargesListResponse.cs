namespace Coditech.Common.API.Model.Response
{
    public class HospitalDoctorVisitingChargesListResponse : BaseListResponse
    {
        public List<HospitalDoctorVisitingChargesModel> HospitalDoctorVisitingChargesList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
