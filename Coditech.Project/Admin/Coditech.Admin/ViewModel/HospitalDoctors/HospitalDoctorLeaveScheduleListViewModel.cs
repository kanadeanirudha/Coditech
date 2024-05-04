using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorLeaveScheduleListViewModel : BaseViewModel
    {
        public List<HospitalDoctorLeaveScheduleViewModel> HospitalDoctorLeaveScheduleList { get; set; }
        public HospitalDoctorLeaveScheduleListViewModel()
        {
            HospitalDoctorLeaveScheduleList = new List<HospitalDoctorLeaveScheduleViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
