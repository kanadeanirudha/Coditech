using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralNationalityViewModel : BaseViewModel
    {
        public short GeneralNationalityMasterId { get; set; }
        [Display(Name = "Nationality")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }
    }
}
