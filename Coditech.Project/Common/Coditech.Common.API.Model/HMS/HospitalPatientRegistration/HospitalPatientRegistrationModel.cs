namespace Coditech.Common.API.Model
{
    public class HospitalPatientRegistrationModel : BaseModel
    {
        public long HospitalPatientRegistrationId { get; set; }
        public byte HospitalPatientTypeId { get; set; }
        public long PersonId { get; set; }
        public string UAHNumber { get; set; }
        public string UserType { get; set; }
        public string CentreCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string FullNameWithPersonCode { get; set; }
        public string Gender { get; set; }
    }
}
