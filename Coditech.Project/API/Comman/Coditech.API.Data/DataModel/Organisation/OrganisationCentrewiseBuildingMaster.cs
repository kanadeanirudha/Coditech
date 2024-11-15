using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseBuildingMaster
    {
        [Key]
        public int OrganisationCentrewiseBuildingMasterId { get; set; }
        public string CentreCode { get; set; }
        public string BuildingName { get; set; }
        public short Area { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
