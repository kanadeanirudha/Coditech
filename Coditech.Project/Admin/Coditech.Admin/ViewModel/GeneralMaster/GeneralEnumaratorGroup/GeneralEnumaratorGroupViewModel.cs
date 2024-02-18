using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorGroupViewModel : BaseViewModel
    {
        public int GeneralEnumaratorGroupId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Enum Group Code")]
        public string EnumGroupCode { get; set; }

        [Display(Name = "Display Text")]
        [Required]
        [MaxLength(50)]
        public string DisplayText { get; set; }

        public List<GeneralEnumaratorModel> GeneralEnumaratorList { get; set; }
    }
}
