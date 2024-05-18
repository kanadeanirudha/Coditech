namespace Coditech.Common.API.Model
{
    public class HospitalPatientRegistrationListModel : BaseListModel
    {
        public List<HospitalPatientRegistrationModel> HospitalPatientRegistrationList { get; set; }
        public HospitalPatientRegistrationListModel()
        {
            HospitalPatientRegistrationList = new List<HospitalPatientRegistrationModel>();
        }

    }
}
