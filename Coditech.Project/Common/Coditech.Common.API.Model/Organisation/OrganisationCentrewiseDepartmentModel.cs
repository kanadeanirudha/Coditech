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
        public string CentreCode { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        public Nullable<int> DepartmentSeqNo { get; set; }
    }
}
