namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLTypeModel : BaseModel
    {
        public short AccSetupGLTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public short SequenceNumber { get; set; }
        public bool IsCentreDefaultGLType { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
    }
}
