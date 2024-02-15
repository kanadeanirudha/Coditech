using Coditech.Common.API.Model.Response;

namespace Coditech.Common.API.Model.Responses
{
    public class GeneralPersonAttendanceDetailsListResponse : BaseListResponse
    {
        public List<GeneralPersonAttendanceDetailsModel> GeneralPersonAttendanceDetailsList { get; set; }
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
