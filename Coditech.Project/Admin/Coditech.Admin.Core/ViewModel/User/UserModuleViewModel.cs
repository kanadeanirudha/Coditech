using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserModuleViewModel : BaseViewModel
    {
        public short UserModuleMasterId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Code")]
        [Required]
        public string ModuleCode { get; set; }

        [MaxLength(60)]
        [Display(Name = "Module Name")]
        [Required]
        public string ModuleName { get; set; }

        [Display(Name = "Module Seq Number")]
        public int? ModuleSeqNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Tool tip")]
        public string ModuleTooltip { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Icon Name")]
        public string ModuleIconName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Color Class")]
        public string ModuleColorClass { get; set; }
        [Display(Name = "Default Menu Link")]
        public string DefaultMenuLink { get; set; }
        [MaxLength(50)]
        [Display(Name = "Module Related With")]
        public string ModuleRelatedWith { get; set; }
        [MaxLength(50)]
        [Display(Name = "Module Form Name")]
        public string ModuleFormName { get; set; }
        [MaxLength(100)]
        [Display(Name = "Module Icon Path")]
        public string ModuleIconPath { get; set; }
        [Display(Name = "Module Installed Flag")]
        public bool ModuleInstalledFlag { get; set; }
        [Display(Name = "Module Active Flag")]
        public bool ModuleActiveFlag { get; set; }
    }
}