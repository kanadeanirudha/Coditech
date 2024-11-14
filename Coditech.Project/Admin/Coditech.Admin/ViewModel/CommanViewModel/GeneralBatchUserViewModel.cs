using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralBatchUserViewModel : BaseViewModel
    {
        public long GeneralBatchUserId { get; set; }
        public int GeneralBatchMasterId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public bool IsAssociated { get; set; }
        public string BatchName { get; set; }
    }
}
