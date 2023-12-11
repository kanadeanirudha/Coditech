using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorGroupViewModel : BaseViewModel
    {
        public int GeneralEnumaratorGroupId { get; set; }
        [Required]
        [Display(Name = "EnumGroup")]
        public string EnumGroup { get; set; }

        [Display(Name = "Enumarator")]
        [Required]
        public string DisaplyText { get; set; }
        
        [Display(Name = "Created by")]
        public new int CreatedBy { get; set; }

        [Display(Name = "Modified by ")]
        [Required]
        public new int ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public new DateTime ModifiedDate { get; set; }
    }
}
