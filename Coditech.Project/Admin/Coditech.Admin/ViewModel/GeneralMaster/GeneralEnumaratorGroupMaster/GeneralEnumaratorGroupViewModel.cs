using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorGroupViewModel : BaseViewModel
    {
        public int GeneralEnumaratorGroupId { get; set; }
        [Required]
        [Display(Name = "EnumGroupCode Name")]
        public string DisaplyText { get; set; }

        [Display(Name = "EnumGroup Code")]
        [Required]
        public string EnumGroupCode { get; set; }
               
    }
}
