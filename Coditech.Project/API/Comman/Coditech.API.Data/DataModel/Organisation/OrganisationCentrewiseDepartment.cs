using System;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseDepartment
    {
        public int OrganisationCentrewiseDepartmentId { get; set; }
        public short GeneralDepartmentMasterId { get; set; }
        public string CentreCode { get; set; }
        public bool ActiveFlag { get; set; }
        public Nullable<int> DepartmentSeqNo { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
