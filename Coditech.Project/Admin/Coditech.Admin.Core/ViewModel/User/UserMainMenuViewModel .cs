using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserMainMenuViewModel : BaseViewModel
    {
        [Required]
        public short UserMainMenuMasterId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Menu Code")]
        public string MenuCode { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        public short? MenuInnerLevel { get; set; }

        [Display(Name = "Menu Display Seq No")]
        public int? MenuDisplaySeqNo { get; set; }

        [MaxLength(60)]
        public string MenuVerNo { get; set; }

        [Display(Name = "Menu Installed Flag")]
        public bool MenuInstalledFlag { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Display(Name = " Is Enable")]
        public bool IsEnable { get; set; }

        public System.DateTime? DisableDate { get; set; }

        [MaxLength(100)]
        public string RemarkAboutDisable { get; set; }

        [MaxLength(50)]
        [Display(Name = "Menu Tool Tip")]
        public string MenuToolTip { get; set; }

        [MaxLength(100)]
        public string ParentMenuName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Parent Menu Code")]
        public string ParentMenuCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Menu Icon Name")]
        public string MenuIconName { get; set; }
    }
}