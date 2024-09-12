namespace Coditech.Common.API.Model
{
    public class HospitalRegistrationFeeListModel : BaseListModel
    {
        public List<HospitalRegistrationFeeModel> HospitalRegistrationFeeList { get; set; }
        public HospitalRegistrationFeeListModel()
        {
            HospitalRegistrationFeeList = new List<HospitalRegistrationFeeModel>();
        }

    }
}
