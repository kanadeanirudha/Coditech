namespace Coditech.Common.API.Model
{
    public class GeneralPersonModel : BaseModel
    {
        public long PersonId { get; set; }
        public string UserType { get; set; }
        public string PersonCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public short NationalityId{ get; set; }
        public string MaritalStatus { get; set; }
        public string IndentificationNumber { get; set; }
        public string IndentificationEnum { get; set; }
    }
}
