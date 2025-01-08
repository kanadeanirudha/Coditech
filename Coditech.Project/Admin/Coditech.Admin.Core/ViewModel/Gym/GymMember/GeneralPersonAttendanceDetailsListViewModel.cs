using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAttendanceDetailsListViewModel : BaseViewModel
    {
        public List<GeneralPersonAttendanceDetailsViewModel> GeneralPersonAttendanceDetailsList { get; set; }
        public GeneralPersonAttendanceDetailsListViewModel()
        {
            GeneralPersonAttendanceDetailsList = new List<GeneralPersonAttendanceDetailsViewModel>();
        }
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedParameter1 { get; set; }
        public string SelectedParameter2 { get; set; }

    }
}
