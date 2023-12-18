namespace Coditech.Common.API.Model
{
    public class GymMemberDetailsModel : BaseModel
    {
        public int GymMemberDetailId { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
