namespace Coditech.API.Data
{
    public class AccSetupGLType
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
