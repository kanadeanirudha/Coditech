namespace Coditech.Common.API.Model.Response
{
    public class HospitalPatientTimeSlotListResponse : BaseListResponse
    {
        public List<HospitalPatientTimeSlotModel> TimeSlotList { get; set; }
    }
}
