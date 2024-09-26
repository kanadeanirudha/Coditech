
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Data;
namespace Coditech.API.Service
{
    public class DashboardService : BaseService, IDashboardService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminRoleMaster> _adminRoleMasterRepository;

        public DashboardService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Get Dashboard Details by selected Admin Role Master id.

        public virtual DashboardModel GetDashboardDetails(int selectedAdminRoleMasterId, long userMasterId)
        {
            if (selectedAdminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SelectedAdminRoleMasterId"));

            if (userMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserMasterId"));

            int? dashboardFormEnumId = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == selectedAdminRoleMasterId)?.Select(y => y.DashboardFormEnumId)?.FirstOrDefault();
            DashboardModel dashboardModel = new DashboardModel();
            if (dashboardFormEnumId > 0)
            {
                string dashboardFormEnumCode = GetEnumCodeByEnumId((int)dashboardFormEnumId);
                dashboardModel.DashboardFormEnumCode = dashboardFormEnumCode;
                if (dashboardFormEnumCode.Equals(DashboardFormEnum.GymOwnerDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DataSet dataset = GetGymDashboardDetailsByUserId(0);
                    dataset.Tables[0].TableName = "ActiveInActiveDetails";
                    ConvertDataTableToList dataTable = new ConvertDataTableToList();
                    GymDashboardModel gymDashboardModel = dataTable.ConvertDataTable<GymDashboardModel>(dataset.Tables["ActiveInActiveDetails"])?.FirstOrDefault();
                    dashboardModel.GymDashboardModel = gymDashboardModel;
                }
                else if (dashboardFormEnumCode.Equals(DashboardFormEnum.GymOperatorDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DataSet dataset = GetGymDashboardDetailsByUserId(userMasterId);
                    dataset.Tables[0].TableName = "ActiveInActiveDetails";
                    ConvertDataTableToList dataTable = new ConvertDataTableToList();
                    GymDashboardModel gymDashboardModel = dataTable.ConvertDataTable<GymDashboardModel>(dataset.Tables["ActiveInActiveDetails"])?.FirstOrDefault();
                    dashboardModel.GymDashboardModel = gymDashboardModel;
                }
            }

            return dashboardModel;
        }

        protected virtual DataSet GetGymDashboardDetailsByUserId(long userId)
        {
            ExecuteSpHelper objStoredProc = new ExecuteSpHelper(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.GetParameter("@UserId", userId, ParameterDirection.Input, SqlDbType.BigInt);
            return objStoredProc.GetSPResultInDataSet("Coditech_GetGymDashboard");
        }
    }
}
