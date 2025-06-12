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
    public class AccSetupTransactionTypeAgent : BaseAgent, IAccSetupTransactionTypeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupTransactionTypeClient _accSetupTransactionTypeClient;
        #endregion

        #region Public Constructor
        public AccSetupTransactionTypeAgent(ICoditechLogging coditechLogging, IAccSetupTransactionTypeClient accSetupTransactionTypeClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupTransactionTypeClient = GetClient(accSetupTransactionTypeClient);
        }
        #endregion

        #region Public Methods
        public virtual AccSetupTransactionTypeListViewModel GetTransactionTypeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TransactionTypeCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TransactionTypeName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupTransactionTypeListResponse response = _accSetupTransactionTypeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupTransactionTypeListModel accSetupTransactionTypeList = new AccSetupTransactionTypeListModel { AccSetupTransactionTypeList = response?.AccSetupTransactionTypeList };
            AccSetupTransactionTypeListViewModel listViewModel = new AccSetupTransactionTypeListViewModel();
            listViewModel.AccSetupTransactionTypeList = accSetupTransactionTypeList?.AccSetupTransactionTypeList?.ToViewModel<AccSetupTransactionTypeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupTransactionTypeList.Count, BindColumns());
            return listViewModel;
        }

        //Create Acc Setup TransactionType.
        public virtual AccSetupTransactionTypeViewModel CreateTransactionType(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel)
        {
            try
            {
                AccSetupTransactionTypeResponse response = _accSetupTransactionTypeClient.CreateTransactionType(accSetupTransactionTypeViewModel.ToModel<AccSetupTransactionTypeModel>());
                AccSetupTransactionTypeModel accSetupTransactionTypeModel = response?.AccSetupTransactionTypeModel;
                return IsNotNull(accSetupTransactionTypeModel) ? accSetupTransactionTypeModel.ToViewModel<AccSetupTransactionTypeViewModel>() : new AccSetupTransactionTypeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Acc Setup TransactionType by Acc Setup TransactionType id.
        public virtual AccSetupTransactionTypeViewModel GetTransactionType(short accSetupTransactionTypeId)
        {
            AccSetupTransactionTypeResponse response = _accSetupTransactionTypeClient.GetTransactionType(accSetupTransactionTypeId);
            return response?.AccSetupTransactionTypeModel.ToViewModel<AccSetupTransactionTypeViewModel>();
        }

        //Update AccSetupTransactionType.
        public virtual AccSetupTransactionTypeViewModel UpdateTransactionType(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Info);
                AccSetupTransactionTypeResponse response = _accSetupTransactionTypeClient.UpdateTransactionType(accSetupTransactionTypeViewModel.ToModel<AccSetupTransactionTypeModel>());
                AccSetupTransactionTypeModel accSetupTransactionTypeModel = response?.AccSetupTransactionTypeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Info);
                return IsNotNull(accSetupTransactionTypeModel) ? accSetupTransactionTypeModel.ToViewModel<AccSetupTransactionTypeViewModel>() : (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(new AccSetupTransactionTypeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return (AccSetupTransactionTypeViewModel)GetViewModelWithErrorMessage(accSetupTransactionTypeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete AccSetupTransactionType.
        public virtual bool DeleteTransactionType(string accSetupTransactionTypeId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accSetupTransactionTypeClient.DeleteTransactionType(new ParameterModel { Ids = accSetupTransactionTypeId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAccSetupTransactionType;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
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
                ColumnName = "Transaction Type Code",
                ColumnCode = "TransactionTypeCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Transaction Type Name",
                ColumnCode = "TransactionTypeName",
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
        #region
        //it will return get all FinancialYear list from database
        public virtual AccSetupTransactionTypeListResponse GetTransactionTypeList()
        {
            AccSetupTransactionTypeListResponse AccSetupTransactionTypeList = _accSetupTransactionTypeClient.List(null, null, null, 1, int.MaxValue);
            return AccSetupTransactionTypeList?.AccSetupTransactionTypeList?.Count > 0 ? AccSetupTransactionTypeList : new AccSetupTransactionTypeListResponse();
        }
        #endregion
    }
}
