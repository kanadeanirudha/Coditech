namespace Coditech.API.Data
{
    public partial class GymMemberDetailsMaster : BaseDataModel
    {
        public int GymMemberDetailId { get; set; }
        public long GeneralPersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}

