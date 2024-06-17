namespace Coditech.Common.API.Model
{
    public class HospitalDoctorVisitingChargesListModel : BaseListModel
    {
        public List<HospitalDoctorVisitingChargesModel> HospitalDoctorVisitingChargesList { get; set; }
        public HospitalDoctorVisitingChargesListModel()
        {
            HospitalDoctorVisitingChargesList = new List<HospitalDoctorVisitingChargesModel>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
