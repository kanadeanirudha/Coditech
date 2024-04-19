using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

namespace Coditech.Admin.Agents
{
    public class DashboardAgent : BaseAgent, IDashboardAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IDashboardClient _dashboardClient;
        #endregion

        #region Public Constructor
        public DashboardAgent(ICoditechLogging coditechLogging, IDashboardClient dashboardClient)
        {
            _coditechLogging = coditechLogging;
            _dashboardClient = GetClient<IDashboardClient>(dashboardClient);
        }
        #endregion

        #region Public Methods

        //Get Dashboard by general selected Admin Role Master id.
        public virtual DashboardViewModel GetDashboardDetails(int selectedAdminRoleMasterId)
        {
            DashboardResponse response = _dashboardClient.GetDashboardDetails(selectedAdminRoleMasterId);
            return response?.DashboardModel.ToViewModel<DashboardViewModel>();
        }

        #endregion
    }
}
