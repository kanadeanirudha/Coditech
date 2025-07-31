namespace Coditech.Common.API.Model
{
    public class UserProfileModel : BaseModel
    {
        public long UserMasterId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Nullable<int> Age { get; set; }
        public int GenderEnumId { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string MaritalStatus { get; set; }
        public long PhotoMediaId { get; set; }
        public string PhotoMediaPath { get; set; }
        public string PhotoMediaFileName { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short EmployeeDesignationMasterId { get; set; }
        public string Description { get; set; }
    }
}
