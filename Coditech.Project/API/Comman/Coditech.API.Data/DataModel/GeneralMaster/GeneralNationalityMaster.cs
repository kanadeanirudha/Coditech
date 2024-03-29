﻿namespace Coditech.API.Data
{
    public partial class GeneralNationalityMaster
    {
        public short GeneralNationalityMasterId { get; set; }
        public string Description { get; set; }
        public bool DefaultFlag { get; set; }
        public Nullable<bool> IsUserDefined { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

