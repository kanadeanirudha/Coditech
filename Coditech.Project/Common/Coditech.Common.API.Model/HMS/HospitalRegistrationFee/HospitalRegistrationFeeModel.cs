using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalRegistrationFeeModel : BaseModel
    {
        [Required]
        public int HospitalRegistrationFeeId { get; set; }
        [Required]
        public int InventoryGeneralItemLineId { get; set; }

        [MaxLength(15)]
        [Required]
        public string CentreCode { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        public DateTime? UptoDate { get; set; }

        [Required]
        public decimal Charges { get; set; }

        [Required]
        public bool IsTaxExclusive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationService { get; set; }

    }
}
