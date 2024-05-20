using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public class MediaGlobalDisplaySettingModel : BaseModel
    {
        public int GlobalMediaDisplaySettingsId { get; set; }
        public long? MediaId { get; set; }
        [Required]
        public int MaxDisplayItems { get; set; }
        [Required]
        public int MaxSmallThumbnailWidth { get; set; }
        [Required]
        public int MaxSmallWidth { get; set; }
        [Required]
        public int MaxMediumWidth { get; set; }
        [Required]
        public int MaxThumbnailWidth { get; set; }
        [Required]
        public int MaxLargeWidth { get; set; }
        [Required]
        public int MaxCrossSellWidth { get; set; }

        public string MediaPath { get; set; }

        public string DefaultImageName { get; set; }

        public static MediaGlobalDisplaySettingModel GetGlobalMediaDisplaySetting()
        {
            return new MediaGlobalDisplaySettingModel
            {
                MaxSmallThumbnailWidth =38,
                MaxLargeWidth = 800,
                MaxSmallWidth = 250,
                MaxThumbnailWidth = 150,
                MaxMediumWidth = 400,
                MaxCrossSellWidth = 150,
            };
        }
    }
}
