using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentListViewModel : BaseViewModel
    {
        public List<HospitalPatientAppointmentViewModel> HospitalPatientAppointmentList { get; set; }
        public HospitalPatientAppointmentListViewModel()
        {
            HospitalPatientAppointmentList = new List<HospitalPatientAppointmentViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
