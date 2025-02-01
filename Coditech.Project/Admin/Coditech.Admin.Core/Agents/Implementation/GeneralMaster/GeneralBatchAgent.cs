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
    public class GeneralBatchAgent : BaseAgent, IGeneralBatchAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralBatchClient _generalBatchClient;
        #endregion

        #region Public Constructor
        public GeneralBatchAgent(ICoditechLogging coditechLogging, IGeneralBatchClient generalBatchClient)
        {
            _coditechLogging = coditechLogging;
            _generalBatchClient = GetClient< IGeneralBatchClient > (generalBatchClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralBatchListViewModel GetBatchList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("BatchName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("BatchTime", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "BatchName " : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralBatchListResponse response = _generalBatchClient.List(dataTableModel.SelectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralBatchListModel generalBatchList = new GeneralBatchListModel { GeneralBatchList = response?.GeneralBatchList };
            GeneralBatchListViewModel listViewModel = new GeneralBatchListViewModel();
            listViewModel.GeneralBatchList = generalBatchList?.GeneralBatchList?.ToViewModel<GeneralBatchViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralBatchList.Count, BindColumns());
            return listViewModel;
        }

        //Create GeneralBatch.
        public virtual GeneralBatchViewModel CreateGeneralBatch(GeneralBatchViewModel generalBatchViewModel)
        {
            try
            {
                GeneralBatchResponse response = _generalBatchClient.CreateGeneralBatch(generalBatchViewModel.ToModel<GeneralBatchModel>());
                GeneralBatchModel generalBatchModel = response?.GeneralBatchModel;
                return IsNotNull(generalBatchModel) ? generalBatchModel.ToViewModel<GeneralBatchViewModel>() : new GeneralBatchViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralBatchViewModel)GetViewModelWithErrorMessage(generalBatchViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralBatchViewModel)GetViewModelWithErrorMessage(generalBatchViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return (GeneralBatchViewModel)GetViewModelWithErrorMessage(generalBatchViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get General Batch by generalBatchMasterId.
        public virtual GeneralBatchViewModel GetGeneralBatch(int generalBatchMasterId)
        {
            GeneralBatchResponse response = _generalBatchClient.GetGeneralBatch(generalBatchMasterId);
            return response?.GeneralBatchModel.ToViewModel<GeneralBatchViewModel>();
        }

        //Update  General Batch.
        public virtual GeneralBatchViewModel UpdateGeneralBatch(GeneralBatchViewModel generalBatchViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Info);
                GeneralBatchResponse response = _generalBatchClient.UpdateGeneralBatch(generalBatchViewModel.ToModel<GeneralBatchModel>());
                GeneralBatchModel generalBatchModel = response?.GeneralBatchModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Info);
                return IsNotNull(generalBatchModel) ? generalBatchModel.ToViewModel<GeneralBatchViewModel>() : (GeneralBatchViewModel)GetViewModelWithErrorMessage(new GeneralBatchViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return (GeneralBatchViewModel)GetViewModelWithErrorMessage(generalBatchViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete General Batch.
        public virtual bool DeleteGeneralBatch(string generalBatchMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalBatchClient.DeleteGeneralBatch(new ParameterModel { Ids = generalBatchMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteBatchDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region GeneralBatchUser
        public virtual GeneralBatchUserListViewModel GetGeneralBatchUserList(int generalBatchMasterId, string userType, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }


            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralBatchUserListResponse response = _generalBatchClient.GetGeneralBatchUserList(generalBatchMasterId,UserTypeEnum.Trainee.ToString(), null, filters, sortlist, dataTableModel.PageIndex, int.MaxValue);
            GeneralBatchUserListModel generalBatchUserList = new GeneralBatchUserListModel { GeneralBatchUserList = response?.GeneralBatchUserList };
            GeneralBatchUserListViewModel listViewModel = new GeneralBatchUserListViewModel();
            listViewModel.GeneralBatchUserList = generalBatchUserList?.GeneralBatchUserList?.ToViewModel<GeneralBatchUserViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralBatchUserList.Count, BindAssociatedBatchColumns());
           
            listViewModel.GeneralBatchMasterId = generalBatchMasterId;
            listViewModel.BatchName = response.BatchName;
            return listViewModel;
        }

        //Update Associate UnAssociate Batchwise User.
        public virtual GeneralBatchUserViewModel AssociateUnAssociateBatchwiseUser(GeneralBatchUserViewModel generalBatchUserViewModel)
        {
            try
            {
                int generalBatchMasterId = generalBatchUserViewModel.GeneralBatchMasterId;
                long generalBatchUserId = generalBatchUserViewModel.GeneralBatchUserId;
                generalBatchUserViewModel.UserType = UserTypeEnum.Trainee.ToString();
                GeneralBatchUserResponse response = _generalBatchClient.AssociateUnAssociateBatchwiseUser(generalBatchUserViewModel.ToModel<GeneralBatchUserModel>());
                GeneralBatchUserModel generalBatchUserModel = response?.GeneralBatchUserModel;
                generalBatchUserViewModel = IsNotNull(generalBatchUserModel) ? generalBatchUserModel.ToViewModel<GeneralBatchUserViewModel>() : new GeneralBatchUserViewModel();
                generalBatchUserViewModel.GeneralBatchMasterId = generalBatchMasterId;
                generalBatchUserViewModel.GeneralBatchUserId = generalBatchUserId;
                return generalBatchUserViewModel;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatchUser.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralBatchUserViewModel)GetViewModelWithErrorMessage(generalBatchUserViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralBatchUserViewModel)GetViewModelWithErrorMessage(generalBatchUserViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatchUser.ToString(), TraceLevel.Error);
                return (GeneralBatchUserViewModel)GetViewModelWithErrorMessage(generalBatchUserViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Batch Name",
                ColumnCode = "BatchName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Batch Time",
                ColumnCode = "BatchTime",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Batch Start Time",
                ColumnCode = "BatchStartTime",
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

        protected virtual List<DatatableColumns> BindAssociatedBatchColumns()
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
                ColumnName = "Is Associated",
                ColumnCode = "GeneralBatchMasterId",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        #endregion
    }
}
