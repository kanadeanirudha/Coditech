namespace Coditech.Common.API.Model.Response
{
    public class GeneralTraineeAssociatedToTrainerListResponse : BaseListResponse
    {
        public List<GeneralTraineeAssociatedToTrainerModel> AssociatedTrainerList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEntityActive { get; set; }
        public string Custom1 { get; set; }
    }
}
