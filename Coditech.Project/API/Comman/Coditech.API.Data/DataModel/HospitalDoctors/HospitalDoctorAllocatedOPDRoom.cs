using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctorAllocatedOPDRoom
    {
        [Key]
        public int HospitalDoctorAllocatedOPDRoomId { get; set; }
        public int HospitalDoctorId { get; set; }
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

