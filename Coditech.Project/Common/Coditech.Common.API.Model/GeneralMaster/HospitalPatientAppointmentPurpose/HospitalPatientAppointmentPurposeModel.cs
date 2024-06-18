using System;
using System.Numerics;

namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentPurposeModel : BaseModel
    {
        public HospitalPatientAppointmentPurposeModel()
        {
          
        }
        public short HospitalPatientAppointmentPurposeId { get; set; }
        public string HospitalPatientAppointmentPurposeName { get; set; }
        public bool IsActive { get; set; } = false;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}