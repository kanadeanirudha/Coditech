namespace Coditech.Common.API.Model
{
    public class HospitalDoctorAllocatedOPDRoomListModel : BaseListModel
    {
        public List<HospitalDoctorAllocatedOPDRoomModel> HospitalDoctorAllocatedOPDRoomList { get; set; }
        public HospitalDoctorAllocatedOPDRoomListModel()
        {
            HospitalDoctorAllocatedOPDRoomList = new List<HospitalDoctorAllocatedOPDRoomModel>();
        }
    }
}
