using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaTypeExtensionMaster
    {
        [Key]
        public short MediaTypeExtensionMasterId { get; set; }
        public byte MediaTypeMasterId { get; set; }
        public string ExtensionName { get; set; }
    }
}
