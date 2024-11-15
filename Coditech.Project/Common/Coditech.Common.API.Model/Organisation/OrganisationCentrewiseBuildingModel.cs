using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseBuildingModel : BaseModel
    {
        public int OrganisationCentrewiseBuildingMasterId { get; set; }

        [MaxLength(15)]
        [Required]
        public string CentreCode { get; set; }

        [MaxLength(100)]
        [Required]
        public string BuildingName { get; set; }      
        
        public short Area { get; set; }
        
    }
} 
