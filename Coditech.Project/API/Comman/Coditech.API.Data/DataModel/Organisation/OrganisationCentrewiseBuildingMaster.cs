using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseBuildingMaster
    {
        [Key]
        public short OrganisationCentrewiseBuildingMasterId { get; set; }
        public string CentreCode { get; set; }
        public string BuildName { get; set; }
        public short Area { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
