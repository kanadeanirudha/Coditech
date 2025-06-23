using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupTransactionTypeViewModel : BaseViewModel
    {
        public short AccSetupTransactionTypeId { get; set; }

        [Display(Name = "Transaction Type Code")]
        [Required]
        public string TransactionTypeCode { get; set; }

        [Display(Name = "Transaction Type Name")]
        [Required]
        public string TransactionTypeName { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


    }
}
