namespace Coditech.Common.API.Model.Response
{
    public class GeneralPersonAttendanceDetailsListResponse : BaseListResponse
    {
        public List<GeneralPersonAttendanceDetailsModel> GeneralPersonAttendanceDetailsList { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
