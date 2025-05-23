﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class MediaConfigurationModel : BaseModel
    {
        public byte MediaConfigurationId { get; set; }
        public int? MediaServerMasterId { get; set; }
        [Required]
        public string Server { get; set; }
        public string URL { get; set; }
        public string NetworkUrl { get; set; }
        public string SecretKey { get; set; }
        public string AccessKey { get; set; }
        public string BucketName { get; set; }
        public bool IsActive { get; set; }
        public string CDNUrl { get; set; }
        public string ThumbnailFolderName { get; set; }
        public MediaServerModel MediaServer { get; set; }
        public MediaGlobalDisplaySettingModel GlobalMediaDisplaySetting { get; set; }
    }
}
