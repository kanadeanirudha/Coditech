using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class CoditechApplicationSetting
    {
        [Key]
        public short CoditechApplicationSettingId { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationValue1 { get; set; }
        public string ApplicationValue2 { get; set; }
        public string ApplicationValue3 { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

