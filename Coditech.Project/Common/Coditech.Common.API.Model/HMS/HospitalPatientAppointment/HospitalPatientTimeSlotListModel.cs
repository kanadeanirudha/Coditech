namespace Coditech.Common.API.Model
{
    public class HospitalPatientTimeSlotListModel : BaseListModel
    {
        public List<HospitalPatientTimeSlotModel> TimeSlotList { get; set; }
        public HospitalPatientTimeSlotListModel()
        {
            TimeSlotList = new List<HospitalPatientTimeSlotModel>();
        }
    }
}
