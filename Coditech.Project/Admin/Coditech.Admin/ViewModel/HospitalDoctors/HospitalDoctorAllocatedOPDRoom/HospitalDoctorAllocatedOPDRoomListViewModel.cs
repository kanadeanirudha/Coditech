using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorAllocatedOPDRoomListViewModel : BaseViewModel
    {
        public List<HospitalDoctorAllocatedOPDRoomViewModel> HospitalDoctorAllocatedOPDRoomList { get; set; }
        public HospitalDoctorAllocatedOPDRoomListViewModel()
        {
            HospitalDoctorAllocatedOPDRoomList = new List<HospitalDoctorAllocatedOPDRoomViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
