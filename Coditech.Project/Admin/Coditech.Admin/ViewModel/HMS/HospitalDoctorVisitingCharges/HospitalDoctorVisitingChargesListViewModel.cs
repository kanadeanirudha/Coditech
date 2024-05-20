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
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
