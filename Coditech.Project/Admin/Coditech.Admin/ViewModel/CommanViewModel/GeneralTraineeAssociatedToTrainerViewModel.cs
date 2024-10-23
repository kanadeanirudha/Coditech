using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTraineeAssociatedToTrainerViewModel : BaseViewModel
    {
        public long GeneralTraineeAssociatedToTrainerId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        [Required]
        [Display(Name = "Trainer")]
        public long GeneralTrainerMasterId { get; set; }
        [Display(Name = "Is Current Trainer")]
        public bool IsCurrentTrainer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public string EmailId { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        public long PersonId { get; set; }
    }
}
