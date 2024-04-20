namespace Coditech.Common.API.Model
{
    public class DashboardModel : BaseModel
    {
        public DashboardModel()
        {
        }
        public string DashboardFormEnumCode { get; set; }
        public GymDashboardModel GymDashboardModel { get; set; }
    }
}
