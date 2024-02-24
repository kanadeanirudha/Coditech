
namespace Coditech.Common.API.Model
{
    public class GeneralPersonAttendanceDetailsListModel : BaseListModel
    {
        public List<GeneralPersonAttendanceDetailsModel> GeneralPersonAttendanceDetailsList { get; set; }
        public GeneralPersonAttendanceDetailsListModel()
        {
            GeneralPersonAttendanceDetailsList = new List<GeneralPersonAttendanceDetailsModel>();
        }
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long EntityId { get; set; }
    }
}
