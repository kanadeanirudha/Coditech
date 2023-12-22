using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaConfiguration
    {
        [Key]
        public byte MediaConfigurationId { get; set; }
        public byte MediaServerMasterId { get; set; }
        public string Server { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string URL { get; set; }
        public string CDNUrl { get; set; }
        public string BucketName { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public bool IsActive { get; set; } = true;
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
