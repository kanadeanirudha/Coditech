﻿namespace Coditech.API.Data
{
    public partial class GeneralSystemGlobleSettingMaster
    {
        public short GeneralSystemGlobleSettingMasterId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDefaultValue { get; set; }
        public string FeatureValue { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

