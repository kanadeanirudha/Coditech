namespace Coditech.Common.API.Model
{
    public class UserMobileAppModel : BaseModel
    {
        public long UserMasterId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
