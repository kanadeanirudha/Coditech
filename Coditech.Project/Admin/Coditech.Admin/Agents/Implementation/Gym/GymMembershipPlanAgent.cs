using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GymMembershipPlanAgent : BaseAgent, IGymMembershipPlanAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGymMembershipPlanClient _gymMembershipPlanClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GymMembershipPlanAgent(ICoditechLogging coditechLogging, IGymMembershipPlanClient gymMembershipPlanClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _gymMembershipPlanClient = GetClient<IGymMembershipPlanClient>(gymMembershipPlanClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual GymMembershipPlanListViewModel GetGymMembershipPlanList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMembershipPlanListResponse response = _gymMembershipPlanClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMembershipPlanListModel gymMemberList = new GymMembershipPlanListModel { GymMembershipPlanList = response?.GymMembershipPlanList };
            GymMembershipPlanListViewModel listViewModel = new GymMembershipPlanListViewModel();
            listViewModel.GymMembershipPlanList = gymMemberList?.GymMembershipPlanList?.ToViewModel<GymMembershipPlanViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMembershipPlanList.Count, BindColumns());
            return listViewModel;
        }

        #region General Person
        //Create Member shipPlan
        public virtual GymCreateEditMemberViewModel CreateMembershipPlan(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                gymCreateEditMemberViewModel.UserType = UserTypeEnum.GymMember.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(gymCreateEditMemberViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<GymCreateEditMemberViewModel>() : new GymCreateEditMemberViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, ex.ErrorMessage);
                    default:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Member Personal shipPlan by personId.
        public virtual GymCreateEditMemberViewModel GetMemberPersonalshipPlan(long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            return response?.GeneralPersonModel.ToViewModel<GymCreateEditMemberViewModel>();
        }

        //Update Member shipPlan
        public virtual GymCreateEditMemberViewModel UpdateMemberPersonalshipPlan(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(gymCreateEditMemberViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<GymCreateEditMemberViewModel>() : (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(new GymCreateEditMemberViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        #region Member Other shipPlan
        //Get Member Other shipPlan
        public virtual GymMembershipPlanViewModel GetGymMembershipPlan(int gymMemberDetailId)
        {
            GymMembershipPlanResponse response = _gymMembershipPlanClient.GetGymMembershipPlan(gymMemberDetailId);
            GymMembershipPlanViewModel gymMembershipPlanViewModel = response?.GymMembershipPlanModel.ToViewModel<GymMembershipPlanViewModel>();
            return gymMembershipPlanViewModel;
        }

        //Update Gym Member Other shipPlan.
        public virtual GymMembershipPlanViewModel UpdateGymMembershipPlan(GymMembershipPlanViewModel gymMembershipPlanViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                GymMembershipPlanResponse response = _gymMembershipPlanClient.UpdateGymMembershipPlan(gymMembershipPlanViewModel.ToModel<GymMembershipPlanModel>());
                GymMembershipPlanModel gymMembershipPlanModel = response?.GymMembershipPlanModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                return IsNotNull(gymMembershipPlanModel) ? gymMembershipPlanModel.ToViewModel<GymMembershipPlanViewModel>() : (GymMembershipPlanViewModel)GetViewModelWithErrorMessage(new GymMembershipPlanViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymMembershipPlanViewModel)GetViewModelWithErrorMessage(gymMembershipPlanViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete gym Member shipPlan.
        public virtual bool DeleteGymMembers(string gymMemberDetailIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _gymMembershipPlanClient.DeleteGymMembers(new ParameterModel { Ids = gymMemberDetailIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGymMembershipPlan;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region Member Follow Up
        public virtual GymMemberFollowUpListViewModel GymMemberFollowUpList(int gymMemberDetailId, long personId, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FollowupType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FollowupComment", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMemberFollowUpListResponse response = _gymMembershipPlanClient.GymMemberFollowUpList(gymMemberDetailId, personId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMemberFollowUpListModel gymMemberList = new GymMemberFollowUpListModel { GymMemberFollowUpList = response?.GymMemberFollowUpList };

            GymMemberFollowUpListViewModel listViewModel = new GymMemberFollowUpListViewModel()
            {
                PersonId = response.PersonId,
                GymMemberDetailId = response.GymMemberDetailId,
                FirstName = response?.FirstName,
                LastName = response?.LastName
            };
            listViewModel.GymMemberFollowUpList = gymMemberList?.GymMemberFollowUpList?.ToViewModel<GymMemberFollowUpViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberFollowUpList.Count, BindGymMemberFollowUpColumns());
            return listViewModel;
        }

        #endregion
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Image",
                ColumnCode = "Image",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "First Name",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Last Name",
                ColumnCode = "LastName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Email Id",
                ColumnCode = "EmailId",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        protected virtual List<DatatableColumns> BindGymMemberFollowUpColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Comments",
                ColumnCode = "FollowupComment",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Follow-Up Type",
                ColumnCode = "FollowupType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Set Reminder",
                ColumnCode = "IsSetReminder",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Reminder Date",
                ColumnCode = "ReminderDate",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
