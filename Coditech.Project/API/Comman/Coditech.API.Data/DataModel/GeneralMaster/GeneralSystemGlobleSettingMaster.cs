namespace Coditech.API.Data
{
    public partial class GeneralSystemGlobleSettingMaster : BaseDataModel
    {
        public short GeneralSystemGlobleSettingMasterId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDefaultValue { get; set; }
        public string FeatureValue { get; set; }
    }
}

