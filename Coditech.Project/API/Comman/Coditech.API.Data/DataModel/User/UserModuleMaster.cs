﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class UserModuleMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
