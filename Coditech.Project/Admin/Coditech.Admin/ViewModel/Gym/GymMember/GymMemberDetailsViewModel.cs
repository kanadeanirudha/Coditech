using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberDetailsViewModel : BaseViewModel
    {
        public GymMemberDetailsViewModel()
        {
        }
        public int GymMemberDetailId { get; set; }
        public long PersonId { get; set; }
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string PastInjuries { get; set; }
        public string MedicalHistory { get; set; }
        public string OtherInformation { get; set; }
        public short? GymGroupMasterId { get; set; }
        public int? SourceEmumId { get; set; }
    }
}
