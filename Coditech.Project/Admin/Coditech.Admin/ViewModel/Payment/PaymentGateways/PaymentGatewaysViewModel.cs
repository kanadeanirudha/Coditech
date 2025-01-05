using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class PaymentGatewaysViewModel : BaseViewModel
    {
        public byte PaymentGatewayId { get; set; }
        [MaxLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        [Display(Name = "Payment Gateway Name")]

        public string PaymentName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Payment Code")]
        public string PaymentCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
