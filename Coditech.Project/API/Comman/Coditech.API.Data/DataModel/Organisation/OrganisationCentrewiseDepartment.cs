using System;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseDepartment 
    {
        public short OrganisationCentrewiseDepartmentId { get; set; }
        public short DepartmentId { get; set; }
        public string CentreCode { get; set; }
        public bool ActiveFlag { get; set; }
        public Nullable<int> DepartmentSeqNo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
