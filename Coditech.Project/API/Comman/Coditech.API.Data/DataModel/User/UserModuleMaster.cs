namespace Coditech.API.Data
{
    public partial class UserModuleMaster : BaseDataModel
    {
        public byte UserModuleMasterId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public Nullable<bool> ModuleInstalledFlag { get; set; }
        public Nullable<bool> ModuleActiveFlag { get; set; }
        public Nullable<int> ModuleSeqNumber { get; set; }
        public string ModuleRelatedWith { get; set; }
        public string ModuleTooltip { get; set; }
        public string ModuleIconName { get; set; }
        public string ModuleIconPath { get; set; }
        public string ModuleFormName { get; set; }
        public string ModuleColorClass { get; set; }
    }
}
