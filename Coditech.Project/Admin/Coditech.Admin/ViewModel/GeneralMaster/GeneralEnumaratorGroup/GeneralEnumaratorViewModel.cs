using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorViewModel : BaseViewModel
    {
        public int GeneralEnumaratorId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }

        [Display(Name = "Enum Name")]
        [Required]
        [MaxLength(50)]
        public string EnumName { get; set; }

        [Display(Name = "Disaply Text")]
        [Required]
        [MaxLength(250)]
        public string EnumDisplayText { get; set; }

        [Display(Name = "Related With")]
        [MaxLength(50)]
        public string RelatedWith { get; set; }

        [Display(Name = "Enum Value")]
        public short EnumValue { get; set; }

        [Display(Name = "Sequence Number")]
        [Required]
        public short SequenceNumber { get; set; }

        public bool IsDefault { get; set; }
    }
}
