using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class UserTypeViewModel : BaseViewModel
    {
        public short UserTypeId { get; set; }
        
        [Display(Name = "User Type Code")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string UserTypeCode { get; set; }

        [Display(Name = "User Description")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string UserDescription { get; set; }
        [Display(Name = "Related With")]
        public string RelatedWith { get; set; }
        [Display(Name = "Is Common")]
        public bool IsCommon { get; set; }

        [Display(Name = "Is Login Required")]
        [Required]
        public bool IsLoginRequired { get; set; }

        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }
    }
}
