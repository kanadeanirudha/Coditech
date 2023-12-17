using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralPerson : BaseDataModel
    {
        [Key]
        public long PersonId { get; set; }
        public string Title { get; set; }
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
        public string BirthMark { get; set; }
        public string AttendanceIntegrationId { get; set; }
        public short GeneralOccupationMasterId { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
    }
}
