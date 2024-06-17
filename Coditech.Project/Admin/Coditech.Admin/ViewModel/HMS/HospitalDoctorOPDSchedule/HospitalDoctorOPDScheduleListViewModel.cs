using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorOPDScheduleListViewModel : BaseViewModel
    {
        public List<HospitalDoctorOPDScheduleViewModel> HospitalDoctorList { get; set; }
        public HospitalDoctorOPDScheduleListViewModel()
        {
            HospitalDoctorList = new List<HospitalDoctorOPDScheduleViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
