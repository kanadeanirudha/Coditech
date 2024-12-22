namespace Coditech.Common.API.Model
{
    public class MediaGlobalDisplaySettingModel : BaseModel
    {
        public int GlobalMediaDisplaySettingsId { get; set; }
        public long? MediaId { get; set; }
        public int? MaxDisplayItems { get; set; }
        public int? MaxSmallThumbnailWidth { get; set; }
        public int? MaxSmallWidth { get; set; }
        public int? MaxMediumWidth { get; set; }
        public int? MaxThumbnailWidth { get; set; }
        public int? MaxLargeWidth { get; set; }
        public int? MaxCrossSellWidth { get; set; }
        public string MediaPath { get; set; }
        public string DefaultImageName { get; set; }

        public static MediaGlobalDisplaySettingModel GetGlobalMediaDisplaySetting()
        {
            return new MediaGlobalDisplaySettingModel
            {
                MaxSmallThumbnailWidth = 38,
                MaxLargeWidth = 800,
                MaxSmallWidth = 250,
                MaxThumbnailWidth = 150,
                MaxMediumWidth = 400,
                MaxCrossSellWidth = 150,
            };
        }
    }
}
