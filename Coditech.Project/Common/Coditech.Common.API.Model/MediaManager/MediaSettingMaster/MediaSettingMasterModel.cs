namespace Coditech.Common.API.Model
{
    public class MediaSettingMasterModel : BaseModel
    {
        public byte MediaSettingMasterId { get; set; }
        public byte? MediaTypeMasterId { get; set; }
        public string MediaType { get; set; }
        public byte MediaConfigurationId { get; set; }
        public Int16 MaxSizeInMB { get; set; }
        public string MediaTypeExtensionMasterIds { get; set; }
        public List<string> SelectedMediaTypeExtensionMasterIds { get; set; }
        public Int16 LargeImageResize { get; set; }
        public Int16 MediumImageResize { get; set; }
        public Int16 SmallImageResize { get; set; }
        public Int16 CrossSellImageResize { get; set; }
        public Int16 ThumbnailImageResize { get; set; }
        public Int16 SmallThumbnailImageResize { get; set; }
        public string HelpDescription { get; set; }
        public bool IsActive { get; set; }
        public List<MediaTypeExtensionModel> MediaTypeExtensionList { get; set; }
    }
}
