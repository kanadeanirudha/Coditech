namespace Coditech.Common.API.Model
{
    public class HospitalPatientTypeModel : BaseModel
    {
        public HospitalPatientTypeModel()
        {

        }
        public byte HospitalPatientTypeId { get; set; }
        public string PatientType { get; set; }
        public bool IsActive { get; set; }
    }
}
