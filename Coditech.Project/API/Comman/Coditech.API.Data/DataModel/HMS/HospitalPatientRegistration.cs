using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalPatientRegistration
    {
        [Key]
        public long HospitalPatientRegistrationId { get; set; }
        public long PersonId { get; set; }
        public string UAHNumber { get; set; }
        public string CentreCode { get; set; }
        public string UserType { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long? HospitalPatientPreRegistrationId { get; set; }
        public long? HospitalPatientFirstVisitId { get; set; }
        public long? HospitalPatientLastVisitId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}



