using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTraineeAssociatedToTrainerListViewModel : BaseViewModel
    {
        public List<GeneralTraineeAssociatedToTrainerViewModel> AssociatedTrainerList { get; set; }
        public GeneralTraineeAssociatedToTrainerListViewModel()
        {
            AssociatedTrainerList = new List<GeneralTraineeAssociatedToTrainerViewModel>();
        }
        public long DBTMTraineeDetailId { get; set; }
        public long GeneralTrainerMasterId { get; set; }
        public long PersonId { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedParameter1 { get; set; } 
        public string SelectedParameter2 { get; set; }
        public bool IsEntityActive { get; set; }
    }
}
