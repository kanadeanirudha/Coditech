using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseDepartmentViewModel : BaseViewModel
    {
        public int OrganisationCentrewiseDepartmentId { get; set; }

        public short GeneralDepartmentMasterId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsAssociated { get; set; }

        public string DepartmentShortCode { get; set; }
        public string CentreCode { get; set; }
        public string CentreName { get; set; }
    }
}
