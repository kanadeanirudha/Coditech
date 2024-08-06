using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class MediaTypeExtensionModel
    {
        public short MediaTypeExtensionMasterId { get; set; }
        public byte MediaTypeMasterId { get; set; }
        public string ExtensionName { get; set; }
    }
}
