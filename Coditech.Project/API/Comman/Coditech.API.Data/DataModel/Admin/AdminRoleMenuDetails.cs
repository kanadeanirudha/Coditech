﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleMenuDetails
    {
        [Key]
        public int AdminRoleMenuDetailId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string ModuleCode { get; set; }
        public string MenuCode { get; set; }
        public Nullable<System.DateTime> EnableDate { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string DisablePurpose { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
