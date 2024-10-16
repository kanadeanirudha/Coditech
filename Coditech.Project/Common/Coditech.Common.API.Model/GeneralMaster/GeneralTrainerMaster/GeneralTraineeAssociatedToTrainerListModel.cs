namespace Coditech.Common.API.Model
{
    public class GeneralTraineeAssociatedToTrainerListModel : BaseListModel
    {
        public List<GeneralTraineeAssociatedToTrainerModel> AssociatedTrainerList { get; set; }
        public GeneralTraineeAssociatedToTrainerListModel()
        {
            AssociatedTrainerList = new List<GeneralTraineeAssociatedToTrainerModel>();
        }
        public long EntityId { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
