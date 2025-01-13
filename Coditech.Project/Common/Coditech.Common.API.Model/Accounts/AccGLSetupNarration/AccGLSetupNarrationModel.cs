using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public class AccGLSetupNarrationModel : BaseModel
    {
        public int AccGLSetupNarrationId { get; set; }
        public string NarrationDescription { get; set; }
        public string NarrationType { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }

    }
}
