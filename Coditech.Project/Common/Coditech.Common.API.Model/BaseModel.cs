using System.Xml.Serialization;

namespace Coditech.Common.API.Model
{
    public class BaseModel
    {
        [XmlIgnore]
        public int CreatedBy { get; set; }
        [XmlIgnore]
        public DateTime CreatedDate { get; set; }
        [XmlIgnore]
        public int ModifiedBy { get; set; }
        [XmlIgnore]
        public DateTime ModifiedDate { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }
    }
}
