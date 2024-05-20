namespace Coditech.Common.API.Model
{
    public class HospitalDoctorVisitingChargesListModel : BaseListModel
    {
        public List<HospitalDoctorVisitingChargesModel> HospitalDoctorVisitingChargesList { get; set; }
        public HospitalDoctorVisitingChargesListModel()
        {
            HospitalDoctorVisitingChargesList = new List<HospitalDoctorVisitingChargesModel>();
        }
    }
}
