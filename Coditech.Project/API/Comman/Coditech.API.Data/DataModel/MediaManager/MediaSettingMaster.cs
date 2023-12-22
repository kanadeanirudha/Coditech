using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaSettingMaster
    {
        [Key]
        public byte MediaSettingMasterId { get; set; }
        public byte MediaTypeMasterId { get; set; }
        public byte MediaConfigurationId { get; set; }
        public short MaxSizeInMB { get; set; }
        public string MediaTypeExtensionMasterIds { get; set; }
        public short LargeImageResize { get; set; }
        public short MediumImageResize { get; set; }
        public short SmallImageResize { get; set; }
        public short CrossSellImageResize { get; set; }
        public short ThumbnailImageResize { get; set; }
        public short SmallThumbnailImageResize { get; set; }
        public string HelpDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
