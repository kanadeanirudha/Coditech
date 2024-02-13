namespace Coditech.Common.API.Model
{
    public class GymMemberBodyMeasurementListModel : BaseListModel
    {
        public List<GymMemberBodyMeasurementModel> GymMemberBodyMeasurementList { get; set; }
        public GymMemberBodyMeasurementListModel()
        {
            GymMemberBodyMeasurementList = new List<GymMemberBodyMeasurementModel>();
        }

    }
}
