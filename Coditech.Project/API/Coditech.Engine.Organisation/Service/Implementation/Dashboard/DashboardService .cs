﻿
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

                    dataset.Tables[1].TableName = "FinancialOverview";
                    gymDashboardModel.TransactionOverviewList = new List<GymTransactionOverviewModel>();
                    gymDashboardModel.TransactionOverviewList = dataTable.ConvertDataTable<GymTransactionOverviewModel>(dataset.Tables["FinancialOverview"])?.ToList();

                    dataset.Tables[2].TableName = "MembershipPlanExpirationMembersActivity";
                    gymDashboardModel.GymUpcomingPlanExpirationMembersList = new List<GymUpcomingPlanExpirationMembersModel>();
                    gymDashboardModel.GymUpcomingPlanExpirationMembersList = dataTable.ConvertDataTable<GymUpcomingPlanExpirationMembersModel>(dataset.Tables["MembershipPlanExpirationMembersActivity"])?.ToList();

                    dataset.Tables[3].TableName = "RevenueByPaymentMode";
                    gymDashboardModel.RevenueByPaymentModeList = new List<GymTransactionOverviewModel>();
                    gymDashboardModel.RevenueByPaymentModeList = dataTable.ConvertDataTable<GymTransactionOverviewModel>(dataset.Tables["RevenueByPaymentMode"])?.ToList();

                    dataset.Tables[4].TableName = "LeadSource";
                    gymDashboardModel.GymGeneralLeadGenerationSourceList = new List<GymGeneralLeadGenerationSourceModel>();
                    gymDashboardModel.GymGeneralLeadGenerationSourceList = dataTable.ConvertDataTable<GymGeneralLeadGenerationSourceModel>(dataset.Tables["LeadSource"])?.ToList();


                    dataset.Tables[5].TableName = "GymUpComingEvents";
                    gymDashboardModel.GymUpcomingEventsList = new List<GymUpcomingEventsModel>();
                    gymDashboardModel.GymUpcomingEventsList = dataTable.ConvertDataTable<GymUpcomingEventsModel>(dataset.Tables["GymUpComingEvents"])?.ToList();

                    dataset.Tables[6].TableName = "YearlyFinancialOverview";
                    gymDashboardModel.YearlyFinancialOverviewList = new List<GymTransactionOverviewModel>();
                    gymDashboardModel.YearlyFinancialOverviewList = dataTable.ConvertDataTable<GymTransactionOverviewModel>(dataset.Tables["YearlyFinancialOverview"])?.ToList();

                    dashboardModel.GymDashboardModel = gymDashboardModel;

                }
                else if (dashboardFormEnumCode.Equals(DashboardFormEnum.GymOperatorDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DataSet dataset = GetGymDashboardDetailsByUserId(userMasterId);
                    dataset.Tables[0].TableName = "ActiveInActiveDetails";
                    ConvertDataTableToList dataTable = new ConvertDataTableToList();
                    GymDashboardModel gymDashboardModel = dataTable.ConvertDataTable<GymDashboardModel>(dataset.Tables["ActiveInActiveDetails"])?.FirstOrDefault();

                    dataset.Tables[1].TableName = "FinancialOverview";
                    gymDashboardModel.TransactionOverviewList = new List<GymTransactionOverviewModel>();
                    gymDashboardModel.TransactionOverviewList = dataTable.ConvertDataTable<GymTransactionOverviewModel>(dataset.Tables["FinancialOverview"])?.ToList();

                    dataset.Tables[2].TableName = "MembershipPlanExpirationMembersActivity";
                    gymDashboardModel.GymUpcomingPlanExpirationMembersList = new List<GymUpcomingPlanExpirationMembersModel>();
                    gymDashboardModel.GymUpcomingPlanExpirationMembersList = dataTable.ConvertDataTable<GymUpcomingPlanExpirationMembersModel>(dataset.Tables["MembershipPlanExpirationMembersActivity"])?.ToList();

                    dataset.Tables[3].TableName = "RevenueByPaymentMode";
                    gymDashboardModel.RevenueByPaymentModeList = new List<GymTransactionOverviewModel>();
                    gymDashboardModel.RevenueByPaymentModeList = dataTable.ConvertDataTable<GymTransactionOverviewModel>(dataset.Tables["RevenueByPaymentMode"])?.ToList();

                    dataset.Tables[4].TableName = "LeadSource";
                    gymDashboardModel.GymGeneralLeadGenerationSourceList = new List<GymGeneralLeadGenerationSourceModel>();
                    gymDashboardModel.GymGeneralLeadGenerationSourceList = dataTable.ConvertDataTable<GymGeneralLeadGenerationSourceModel>(dataset.Tables["LeadSource"])?.ToList();

                    dataset.Tables[5].TableName = "GymUpComingEvents";
                    gymDashboardModel.GymUpcomingEventsList = new List<GymUpcomingEventsModel>();
                    gymDashboardModel.GymUpcomingEventsList = dataTable.ConvertDataTable<GymUpcomingEventsModel>(dataset.Tables["GymUpComingEvents"])?.ToList();

                    dashboardModel.GymDashboardModel = gymDashboardModel;

                }
                else if (dashboardFormEnumCode.Equals(DashboardFormEnum.DBTMCentreDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DataSet dataset = GetGymDashboardDetailsByUserId(0);
                    var dBTMCentreDashboardModel = new DBTMCentreDashboardModel()
                    {
                        NumberOfTrainees = 120,
                        NumberOfTrainers = 45
                    };

                    dashboardModel.DBTMCentreDashboardModel = dBTMCentreDashboardModel;
                }
                else if (dashboardFormEnumCode.Equals(DashboardFormEnum.DBTMTrainerDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {

                    DataSet dataset = GetGymDashboardDetailsByUserId(userMasterId);
                    var dBTMTrainerDashboardModel = new DBTMTrainerDashboardModel()
                    {
                        NumberOfTrainees = 100,
                        TotalNumberOfActivityPerformedDuringWeek = 30,
                        TopActivityPerformed = "Running",
                        DueTodayAssignments = new List<AssignmentModel>()
                        {
                            new AssignmentModel { AssignmentId = 1, Description = "Running", Status = "InProgress"},
                            new AssignmentModel { AssignmentId = 2, Description = "Weightlifting", Status = "Completed" }
                        },
                        Top3Trainee = new List<TraineeModel>()
                        {
                            new TraineeModel { TraineeId = 1, Name = "Nikita" },
                            new TraineeModel { TraineeId = 2, Name = "Tanuja" },
                            new TraineeModel { TraineeId = 3, Name = "Samruddhi" }
                        }
                    };
                    dashboardModel.DBTMTrainerDashboardModel = dBTMTrainerDashboardModel;
                }
            }
            return dashboardModel;
        }

        protected virtual DataSet GetGymDashboardDetailsByUserId(long userId)
        {
            ExecuteSpHelper objStoredProc = new ExecuteSpHelper(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.GetParameter("@UserId", userId, ParameterDirection.Input, SqlDbType.BigInt);
            objStoredProc.GetParameter("@NumberOfDaysRecord", 30, ParameterDirection.Input, SqlDbType.SmallInt);
            return objStoredProc.GetSPResultInDataSet("Coditech_GetGymDashboard");
        }
    }
}
