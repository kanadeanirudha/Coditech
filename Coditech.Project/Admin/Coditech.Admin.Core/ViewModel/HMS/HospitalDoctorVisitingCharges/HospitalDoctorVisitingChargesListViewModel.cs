using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorVisitingChargesListViewModel : BaseViewModel
    {
        public List<HospitalDoctorVisitingChargesViewModel> HospitalDoctorVisitingChargesList { get; set; }
        public HospitalDoctorVisitingChargesListViewModel()
        {
            HospitalDoctorVisitingChargesList = new List<HospitalDoctorVisitingChargesViewModel>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
        public int HospitalDoctorId { get; set; }
        public string SelectedParameter1 { get; set; }
    }
}
