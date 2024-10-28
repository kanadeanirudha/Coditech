namespace Coditech.Common.API.Model
{
    public class GeneralTraineeAssociatedToTrainerModel : BaseModel
    {
        public long GeneralTraineeAssociatedToTrainerId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public long GeneralTrainerMasterId { get; set; }
        public bool IsCurrentTrainer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public string EmailId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
