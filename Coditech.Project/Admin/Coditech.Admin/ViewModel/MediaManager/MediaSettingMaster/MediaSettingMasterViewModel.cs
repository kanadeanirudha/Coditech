using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class MediaSettingMasterViewModel : BaseViewModel
    {
        public byte MediaSettingMasterId { get; set; }
        
        [Required]
        [Display(Name = "Media Type")]
        public byte MediaTypeMasterId { get; set; }
        public string MediaType { get; set; }

        [Display(Name = "Media Configuration")]
        public byte? MediaConfigurationId { get; set; }
        
        [Display(Name = "Max Size(in MB)")]
        [Required]
        public Int16 MaxSizeInMB { get; set; }
        
        [Display(Name = "Media Type Extension")]
        [Required]
        public List<string> SelectedMediaTypeExtensionMasterIds { get; set; }
        
        [Display(Name = "Large Image")]
        public Int16 LargeImageResize { get; set; }

        [Display(Name = "Medium Image")]
        public Int16 MediumImageResize { get; set; }
        
        [Display(Name = "Small Image")]
        public Int16 SmallImageResize { get; set; }

        [Display(Name = "Cross Sell Image")]
        public Int16 CrossSellImageResize { get; set; }
        
        [Display(Name = "Thumbnail Image")]
        public Int16 ThumbnailImageResize { get; set; }
        [Display(Name = "Small Thumbnail Image")]
        public Int16 SmallThumbnailImageResize { get; set; }
        
        [Display(Name = "Help Description")]
        [MaxLength(100)]
        public string HelpDescription { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
        public List<MediaTypeExtensionModel> MediaTypeExtensionList { get; set; }
    }
}
