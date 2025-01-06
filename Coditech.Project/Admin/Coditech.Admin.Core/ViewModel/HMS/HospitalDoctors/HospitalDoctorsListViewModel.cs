using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorsListViewModel : BaseViewModel
    {
        public List<HospitalDoctorsViewModel> HospitalDoctorsList { get; set; }
        public HospitalDoctorsListViewModel()
        {
            HospitalDoctorsList = new List<HospitalDoctorsViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
