using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseDepartmentModel : BaseModel
    {
        public int OrganisationCentrewiseDepartmentId { get; set; }

        [Required]
        public short GeneralDepartmentMasterId { get; set; }

        public string DepartmentName { get; set; }
        public string CentreCode { get; set; }

        public bool IsAssociated { get; set; }

        public string DepartmentShortCode { get; set; }
    }
}
