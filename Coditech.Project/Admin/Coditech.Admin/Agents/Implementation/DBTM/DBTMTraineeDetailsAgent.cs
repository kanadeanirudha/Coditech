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
    public class DBTMTraineeDetailsAgent : BaseAgent, IDBTMTraineeDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IDBTMTraineeDetailsClient _dBTMTraineeDetailsClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public DBTMTraineeDetailsAgent(ICoditechLogging coditechLogging, IDBTMTraineeDetailsClient dBTMTraineeDetailsClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _dBTMTraineeDetailsClient = GetClient<IDBTMTraineeDetailsClient>(dBTMTraineeDetailsClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual DBTMTraineeDetailsListViewModel GetDBTMTraineeDetailsList(DataTableViewModel dataTableModel, string listType = null)
        {
            FilterCollection filters = new FilterCollection();
            if (listType == "Active")
            {
                filters.Add("IsActive", ProcedureFilterOperators.Equals, "1");
            }
            else if (listType == "InActive")
            {
                filters.Add("IsActive", ProcedureFilterOperators.Equals, "0");
            }
            dataTableModel = dataTableModel ?? new DataTableViewModel();

            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            DBTMTraineeDetailsListResponse response = _dBTMTraineeDetailsClient.List(dataTableModel.SelectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            DBTMTraineeDetailsListModel dBTMTraineeDetailsList = new DBTMTraineeDetailsListModel { DBTMTraineeDetailsList = response?.DBTMTraineeDetailsList };
            DBTMTraineeDetailsListViewModel listViewModel = new DBTMTraineeDetailsListViewModel();
            listViewModel.DBTMTraineeDetailsList = dBTMTraineeDetailsList?.DBTMTraineeDetailsList?.ToViewModel<DBTMTraineeDetailsViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.DBTMTraineeDetailsList.Count, BindColumns());
            return listViewModel;
        }

        #region DBTMTraineeDetails
        //Create DBTMTraineeDetails
        public virtual DBTMTraineeDetailsCreateEditViewModel CreateDBTMTraineeDetails(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel)
        {
            try
            {
                dBTMTraineeDetailsCreateEditViewModel.UserType = UserTypeEnum.DBTMTrainee.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(dBTMTraineeDetailsCreateEditViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<DBTMTraineeDetailsCreateEditViewModel>() : new DBTMTraineeDetailsCreateEditViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (DBTMTraineeDetailsCreateEditViewModel)GetViewModelWithErrorMessage(dBTMTraineeDetailsCreateEditViewModel, ex.ErrorMessage);
                    default:
                        return (DBTMTraineeDetailsCreateEditViewModel)GetViewModelWithErrorMessage(dBTMTraineeDetailsCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Error);
                return (DBTMTraineeDetailsCreateEditViewModel)GetViewModelWithErrorMessage(dBTMTraineeDetailsCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get DBTM Trainee Details by personId.
        public virtual DBTMTraineeDetailsCreateEditViewModel GetDBTMTraineePersonalDetails(long dBTMTraineeDetailId, long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel = response?.GeneralPersonModel.ToViewModel<DBTMTraineeDetailsCreateEditViewModel>();
            if (IsNotNull(dBTMTraineeDetailsCreateEditViewModel))
            {
                DBTMTraineeDetailsResponse dBTMTraineeDetailsResponse = _dBTMTraineeDetailsClient.GetDBTMTraineeOtherDetails(dBTMTraineeDetailId);
                if (IsNotNull(dBTMTraineeDetailsResponse))
                {
                    dBTMTraineeDetailsCreateEditViewModel.SelectedCentreCode = dBTMTraineeDetailsResponse.DBTMTraineeDetailsModel.CentreCode;
                }
                dBTMTraineeDetailsCreateEditViewModel.DBTMTraineeDetailId = dBTMTraineeDetailId;
                dBTMTraineeDetailsCreateEditViewModel.PersonId = personId;
            }
            return dBTMTraineeDetailsCreateEditViewModel;
        }

        //Update DBTM Trainee Details 
        public virtual DBTMTraineeDetailsCreateEditViewModel UpdateDBTMTraineePersonalDetails(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Info);
                GeneralPersonModel generalPersonModel = dBTMTraineeDetailsCreateEditViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.EntityId = dBTMTraineeDetailsCreateEditViewModel.DBTMTraineeDetailId;
                generalPersonModel.UserType = UserTypeEnum.DBTMTrainee.ToString();
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<DBTMTraineeDetailsCreateEditViewModel>() : (DBTMTraineeDetailsCreateEditViewModel)GetViewModelWithErrorMessage(new DBTMTraineeDetailsCreateEditViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Error);
                return (DBTMTraineeDetailsCreateEditViewModel)GetViewModelWithErrorMessage(dBTMTraineeDetailsCreateEditViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        #region Member Other Details
        //Get Member Other Details
        public virtual DBTMTraineeDetailsViewModel GetDBTMTraineeOtherDetails(long dBTMTraineeDetailId)
        {
            DBTMTraineeDetailsResponse response = _dBTMTraineeDetailsClient.GetDBTMTraineeOtherDetails(dBTMTraineeDetailId);
            DBTMTraineeDetailsViewModel dBTMTraineeDetailsViewModel = response?.DBTMTraineeDetailsModel.ToViewModel<DBTMTraineeDetailsViewModel>();
            return dBTMTraineeDetailsViewModel;
        }

        //Update DBTM Trainee Details.
        public virtual DBTMTraineeDetailsViewModel UpdateDBTMTraineeOtherDetails(DBTMTraineeDetailsViewModel dBTMTraineeDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Info);
                DBTMTraineeDetailsResponse response = _dBTMTraineeDetailsClient.UpdateDBTMTraineeOtherDetails(dBTMTraineeDetailsViewModel.ToModel<DBTMTraineeDetailsModel>());
                DBTMTraineeDetailsModel dBTMTraineeDetailsModel = response?.DBTMTraineeDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Info);
                return IsNotNull(dBTMTraineeDetailsModel) ? dBTMTraineeDetailsModel.ToViewModel<DBTMTraineeDetailsViewModel>() : (DBTMTraineeDetailsViewModel)GetViewModelWithErrorMessage(new DBTMTraineeDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Error);
                return (DBTMTraineeDetailsViewModel)GetViewModelWithErrorMessage(dBTMTraineeDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete DBTM Trainee Details .
        public virtual bool DeleteDBTMTraineeDetails(string dBTMTraineeDetailIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _dBTMTraineeDetailsClient.DeleteDBTMTraineeDetails(new ParameterModel { Ids = dBTMTraineeDetailIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteDBTMTraineeDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTraineeDetails.ToString(), TraceLevel.Error);
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
                ColumnName = "Image",
                ColumnCode = "Image",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Person Code",
                ColumnCode = "PersonCode",
                IsSortable = true,
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
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
#endregion