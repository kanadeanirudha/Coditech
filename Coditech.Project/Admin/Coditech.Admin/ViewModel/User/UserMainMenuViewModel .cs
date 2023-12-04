using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserMainMenuViewModel : BaseViewModel
    {
        public short UserMainMenuMasterId { get; set; }

        [MaxLength(50)]
        public string ModuleCode { get; set; }

        [MaxLength(255)]
        public string MenuCode { get; set; }

        [MaxLength(100)]
        public string MenuName { get; set; }

        public short? MenuInnerLevel { get; set; }

        public short? ParentMenuId { get; set; }

        public int? MenuDisplaySeqNo { get; set; }

        [MaxLength(60)]
        public string MenuVerNo { get; set; }

        public bool? MenuInstalledFlag { get; set; }

        [MaxLength(1000)]
        public string ControllerName { get; set; }

        [MaxLength(1000)]
        public string ActionName { get; set; }

        public bool? IsEnable { get; set; }

        public System.DateTime? DisableDate { get; set; }

        [MaxLength(100)]
        public string RemarkAboutDisable { get; set; }

        [MaxLength(50)]
        public string MenuToolTip { get; set; }

        [MaxLength(100)]
        public string ParentMenuName { get; set; }

        [MaxLength(50)]
        public string ParentMenuCode { get; set; }

        [MaxLength(50)]
        public string MenuIconName { get; set; }
    }
}