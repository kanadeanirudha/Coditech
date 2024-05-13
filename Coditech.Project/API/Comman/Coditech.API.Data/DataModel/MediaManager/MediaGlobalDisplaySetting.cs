using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaGlobalDisplaySetting
    {
        [Key]
        public int MediaGlobalDisplaySettingsId { get; set; }
        public long MediaId { get; set; }
        public int MaxDisplayItems { get; set; }
        public int MaxSmallThumbnailWidth { get; set; }
        public int MaxSmallWidth { get; set; }
        public int MaxMediumWidth { get; set; }
        public int MaxThumbnailWidth { get; set; }
        public int MaxLargeWidth { get; set; }
        public int MaxCrossSellWidth { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
