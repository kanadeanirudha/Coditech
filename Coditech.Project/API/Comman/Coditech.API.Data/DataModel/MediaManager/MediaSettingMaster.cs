using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaSettingMaster
    {
        [Key]
        public byte MediaSettingMasterId { get; set; }
        public byte MediaTypeMasterId { get; set; }
        public byte MediaConfigurationId { get; set; }
        public Int16 MaxSizeInMB { get; set; }
        public string MediaTypeExtensionMasterIds { get; set; }
        public Int16 LargeImageResize { get; set; }
        public Int16 MediumImageResize { get; set; }
        public Int16 SmallImageResize { get; set; }
        public Int16 CrossSellImageResize { get; set; }
        public Int16 ThumbnailImageResize { get; set; }
        public Int16 SmallThumbnailImageResize { get; set; }
        public string HelpDescription { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
