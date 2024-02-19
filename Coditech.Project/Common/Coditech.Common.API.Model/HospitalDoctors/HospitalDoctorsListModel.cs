namespace Coditech.Common.API.Model
{
    public class HospitalDoctorsListModel : BaseListModel
    {
        public List<HospitalDoctorsModel> HospitalDoctorsList { get; set; }
        public HospitalDoctorsListModel()
        {
            HospitalDoctorsList = new List<HospitalDoctorsModel>();
        }
    }
}
