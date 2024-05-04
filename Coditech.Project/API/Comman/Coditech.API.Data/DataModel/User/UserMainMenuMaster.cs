using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class UserMainMenuMaster
    {
        [Key]
        public short UserMainMenuMasterId { get; set; }
        public string ModuleCode { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public Nullable<short> MenuInnerLevel { get; set; }
        public Nullable<int> MenuDisplaySeqNo { get; set; }
        public Nullable<bool> MenuInstalledFlag { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string RemarkAboutDisable { get; set; }
        public string MenuToolTip { get; set; }
        public string ParentMenuCode { get; set; }
        public string MenuIconName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
