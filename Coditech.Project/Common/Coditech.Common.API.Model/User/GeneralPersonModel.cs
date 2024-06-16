namespace Coditech.Common.API.Model
{
    public class GeneralPersonModel : BaseModel
    {
        public long PersonId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string PersonCode { get; set; }
        public string PersonTitle { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int GenderEnumId { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmergencyContact { get; set; }
        public short GeneralNationalityMasterId { get; set; }
        public string MaritalStatus { get; set; }
        public string IndentificationNumber { get; set; }
        public int IndentificationEnumId { get; set; }
        public string BloodGroup { get; set; }
        public long PhotoMediaId { get; set; }
        public string PhotoMediaPath { get; set; }
        public string PhotoMediaFileName { get; set; }
        public string BirthMark { get; set; }
        public string AttendanceIntegrationId { get; set; }
        public short GeneralOccupationMasterId { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public string SelectedCentreCode { get; set; }
        public string CentreName { get; set; }
        public string SelectedDepartmentId { get; set; }
        public short EmployeeDesignationMasterId { get; set; }
    }
}
