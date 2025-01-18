using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupTransactionTypeViewModel : BaseViewModel
    {
        public short AccSetupTransactionTypeId { get; set; }

        [Display(Name = "TransactionTypeCode")]
        [Required]
        public string TransactionTypeCode { get; set; }

        [Display(Name = "TransactionTypeName")]
        [Required]
        public  string TransactionTypeName { get; set; }
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }


    }
}
