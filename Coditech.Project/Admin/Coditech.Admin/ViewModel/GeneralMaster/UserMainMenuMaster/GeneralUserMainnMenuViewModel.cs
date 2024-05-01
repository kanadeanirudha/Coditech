using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralUserMainnMenuViewModel : BaseViewModel
    {
        [Required]
        public short UserMainMenuMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "ModuleCode")]
        public string ModuleCode { get; set; }

        [MaxLength(50)]
        public string ParentMenuCode { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "MenuCode")]
        public string MenuCode { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "MenuName")]
        public string MenuName { get; set; }

        public short MenuInnerLevel { get; set; }

        public int? MenuDisplaySeqNo { get; set; }

        public bool MenuInstalledFlag { get; set; }

        [MaxLength(1000)]
        public string ControllerName { get; set; }

        [MaxLength(1000)]
        [Display(Name = "ActionName")]
        public string ActionName { get; set; }

        public bool IsEnable { get; set; }

        public System.DateTime? DisableDate { get; set; }

        [MaxLength(100)]
        public string RemarkAboutDisable { get; set; }

        [MaxLength(50)]
        public string MenuToolTip { get; set; }

        [MaxLength(50)]
        [Display(Name = "MenuIconName")]
        public string MenuIconName { get; set; }
    }
}
