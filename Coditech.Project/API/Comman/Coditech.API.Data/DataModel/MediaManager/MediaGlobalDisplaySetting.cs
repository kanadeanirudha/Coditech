using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaGlobalDisplaySetting
    {
        [Key]
        public int MediaGlobalDisplaySettingsId { get; set; }
        public Nullable<long> MediaId { get; set; }
        public Nullable<int> MaxDisplayItems { get; set; }
        public Nullable<int> MaxSmallThumbnailWidth { get; set; }
        public Nullable<int> MaxSmallWidth { get; set; }
        public Nullable<int> MaxMediumWidth { get; set; }
        public Nullable<int> MaxThumbnailWidth { get; set; }
        public Nullable<int> MaxLargeWidth { get; set; }
        public Nullable<int> MaxCrossSellWidth { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
