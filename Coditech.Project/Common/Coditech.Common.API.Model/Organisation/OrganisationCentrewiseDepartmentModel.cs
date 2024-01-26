using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseDepartmentModel : BaseModel
    {
        [Required]
        public short OrganisationCentrewiseDepartmentId { get; set; }

        [Required]
        public short GeneralDepartmentMasterId { get; set; }

        [MaxLength(15)]
        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public bool IsAssociated { get; set; }

        public string DepartmentShortCode { get; set; }
    }
}
