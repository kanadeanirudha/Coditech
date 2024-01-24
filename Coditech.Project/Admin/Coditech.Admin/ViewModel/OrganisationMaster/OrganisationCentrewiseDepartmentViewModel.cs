using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseDepartmentViewModel : BaseViewModel
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

        public int? DepartmentSeqNo { get; set; }
    }
}
