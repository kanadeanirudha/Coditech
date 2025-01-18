using System.ComponentModel.DataAnnotations;
namespace Coditech.API.Data
{
    public partial class AccGLSetupNarration
    {
        [Key]
        public int AccGLSetupNarrationId { get; set; }
        public string NarrationType { get; set; }
        public string NarrationDescription { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

