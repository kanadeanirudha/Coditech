using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
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
    public class GymWorkoutPlanAgent : BaseAgent, IGymWorkoutPlanAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGymWorkoutPlanClient _gymWorkoutPlanClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GymWorkoutPlanAgent(ICoditechLogging coditechLogging, IGymWorkoutPlanClient gymWorkoutPlanClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _gymWorkoutPlanClient = GetClient<IGymWorkoutPlanClient>(gymWorkoutPlanClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual GymWorkoutPlanListViewModel GetGymWorkoutPlanList(DataTableViewModel dataTableModel, string listType = null)
        {
            FilterCollection filters = new FilterCollection();           
            dataTableModel = dataTableModel ?? new DataTableViewModel();

            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("WorkoutPlanName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Target", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("NumberOfWeeks", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("NumberOfDaysPerWeek", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymWorkoutPlanListResponse response = _gymWorkoutPlanClient.List(dataTableModel.SelectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymWorkoutPlanListModel gymWorkoutPlanList = new GymWorkoutPlanListModel { GymWorkoutPlanList = response?.GymWorkoutPlanList };
            GymWorkoutPlanListViewModel listViewModel = new GymWorkoutPlanListViewModel();
            listViewModel.GymWorkoutPlanList = gymWorkoutPlanList?.GymWorkoutPlanList?.ToViewModel<GymWorkoutPlanViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymWorkoutPlanList.Count, BindColumns());
            return listViewModel;
        }

        //Create Gym Workout Plan .
        public virtual GymWorkoutPlanViewModel CreateGymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            try
            {
                GymWorkoutPlanResponse response = _gymWorkoutPlanClient.CreateGymWorkoutPlan(gymWorkoutPlanViewModel.ToModel<GymWorkoutPlanModel>());
                GymWorkoutPlanModel gymWorkoutPlanModel = response?.GymWorkoutPlanModel;
                return IsNotNull(gymWorkoutPlanModel) ? gymWorkoutPlanModel.ToViewModel<GymWorkoutPlanViewModel>() : new GymWorkoutPlanViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(gymWorkoutPlanViewModel, ex.ErrorMessage);
                    default:
                        return (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(gymWorkoutPlanViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Error);
                return (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(gymWorkoutPlanViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        #region WorkoutPlanDetails

        //Get  Gym Workout Plan details by gymWorkoutPlanId
        public virtual GymWorkoutPlanViewModel GetGymWorkoutPlanDetails(long gymWorkoutPlanId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(gymWorkoutPlanId);
            GymWorkoutPlanViewModel gymWorkoutPlanViewModel = response?.GeneralPersonModel.ToViewModel<GymWorkoutPlanViewModel>();
            if (IsNotNull(gymWorkoutPlanViewModel))
            {
                GymWorkoutPlanResponse gymWorkoutPlanResponse = _gymWorkoutPlanClient.GetGymWorkoutPlan(gymWorkoutPlanId);
                if (IsNotNull(gymWorkoutPlanResponse))
                {
                    gymWorkoutPlanViewModel.CentreCode = gymWorkoutPlanResponse.GymWorkoutPlanModel.CentreCode;
                }
                gymWorkoutPlanViewModel.GymWorkoutPlanId = gymWorkoutPlanId;

            }
            return gymWorkoutPlanViewModel;
        }

        //Update Gym Workout Plan details
        public virtual GymWorkoutPlanViewModel UpdateGymWorkoutPlanDetails(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Info);
                GeneralPersonModel generalPersonModel = gymWorkoutPlanViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.EntityId = gymWorkoutPlanViewModel.GymWorkoutPlanId;
                generalPersonModel.UserType = UserTypeEnum.GymWorkoutPlan.ToString();
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<GymWorkoutPlanViewModel>() : (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(new GymWorkoutPlanViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Error);
                return (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(gymWorkoutPlanViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        #region Gym Workout Plan
        //Get  Gym Workout Plan
        public virtual GymWorkoutPlanViewModel GetGymWorkoutPlan(long gymWorkoutPlanId)
        {
            GymWorkoutPlanResponse response = _gymWorkoutPlanClient.GetGymWorkoutPlan(gymWorkoutPlanId);
            GymWorkoutPlanViewModel gymWorkoutPlanViewModel = response?.GymWorkoutPlanModel.ToViewModel<GymWorkoutPlanViewModel>();
            return gymWorkoutPlanViewModel;
        }

      

        //Update Gym Workout Plan.
        public virtual GymWorkoutPlanViewModel UpdateGymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Info);
                GymWorkoutPlanResponse response = _gymWorkoutPlanClient.UpdateGymWorkoutPlan(gymWorkoutPlanViewModel.ToModel<GymWorkoutPlanModel>());
                GymWorkoutPlanModel gymWorkoutPlanModel = response?.GymWorkoutPlanModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Info);
                return IsNotNull(gymWorkoutPlanModel) ? gymWorkoutPlanModel.ToViewModel<GymWorkoutPlanViewModel>() : (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(new GymWorkoutPlanViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Error);
                return (GymWorkoutPlanViewModel)GetViewModelWithErrorMessage(gymWorkoutPlanViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

      

        //Delete  Gym Workout Plan .
        public virtual bool DeleteGymWorkoutPlan(string gymWorkoutPlanIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _gymWorkoutPlanClient.DeleteGymWorkoutPlan(new ParameterModel { Ids = gymWorkoutPlanIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGymWorkoutPlan;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GymWorkoutPlan.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion



        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Workout Plan Name",
                ColumnCode = "WorkoutPlanName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Target",
                ColumnCode = "Target",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Number Of Weeks",
                ColumnCode = "NumberOfWeeks",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Number Of Days Per Week",
                ColumnCode = "NumberOfDaysPerWeek",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
#endregion