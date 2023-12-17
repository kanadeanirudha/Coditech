using System;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseDepartment : BaseDataModel
    {
        public short OrganisationCentrewiseDepartmentId { get; set; }
        public short GeneralDepartmentMasterId { get; set; }
        public string CentreCode { get; set; }
        public bool ActiveFlag { get; set; }
        public Nullable<int> DepartmentSeqNo { get; set; }
    }
}
