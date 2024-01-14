using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberFollowUpListViewModel : BaseViewModel
    {
        public List<GymMemberFollowUpViewModel> GymMemberFollowUpList { get; set; }
        public GymMemberFollowUpListViewModel()
        {
            GymMemberFollowUpList = new List<GymMemberFollowUpViewModel>();
        }
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
