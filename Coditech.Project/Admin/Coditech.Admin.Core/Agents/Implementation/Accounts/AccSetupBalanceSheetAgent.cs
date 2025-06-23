using System.Diagnostics;
using Coditech.Admin.Utilities;
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
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class AccSetupBalanceSheetAgent : BaseAgent, IAccSetupBalanceSheetAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupBalanceSheetClient _accSetupBalanceSheetClient;
        #endregion

        #region Public Constructor
        public AccSetupBalanceSheetAgent(ICoditechLogging coditechLogging, IAccSetupBalanceSheetClient accSetupBalanceSheetClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupBalanceSheetClient = GetClient<IAccSetupBalanceSheetClient>(accSetupBalanceSheetClient);
        }
        #endregion

        #region Public Methods
        public virtual AccSetupBalanceSheetListViewModel GetBalanceSheetList(DataTableViewModel dataTableModel, byte accSetupBalanceSheetTypeId)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {

                filters.Add("AccBalancesheetHeadDesc", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("AccBalsheetTypeDesc", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupBalanceSheetListResponse response = _accSetupBalanceSheetClient.List(dataTableModel.SelectedCentreCode, accSetupBalanceSheetTypeId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupBalanceSheetListModel balanceSheetList = new AccSetupBalanceSheetListModel { AccSetupBalanceSheetList = response?.AccSetupBalanceSheetList };
            AccSetupBalanceSheetListViewModel listViewModel = new AccSetupBalanceSheetListViewModel();
            listViewModel.AccSetupBalanceSheetList = balanceSheetList?.AccSetupBalanceSheetList?.ToViewModel<AccSetupBalanceSheetViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupBalanceSheetList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Designation.
        public virtual AccSetupBalanceSheetViewModel CreateBalanceSheet(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel)
        {
            try
            {
                AccSetupBalanceSheetResponse response = _accSetupBalanceSheetClient.CreateBalanceSheet(accSetupBalanceSheetViewModel.ToModel<AccSetupBalanceSheetModel>());
                AccSetupBalanceSheetModel accSetupBalanceSheetModel = response?.AccSetupBalanceSheetModel;
                if (!accSetupBalanceSheetModel.HasError)
                {
                    RemoveInSession(AdminConstants.AccountPrerequisiteSession);
                }

                return IsNotNull(accSetupBalanceSheetModel) ? accSetupBalanceSheetModel.ToViewModel<AccSetupBalanceSheetViewModel>() : new AccSetupBalanceSheetViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupBalanceSheetViewModel)GetViewModelWithErrorMessage(accSetupBalanceSheetViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupBalanceSheetViewModel)GetViewModelWithErrorMessage(accSetupBalanceSheetViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return (AccSetupBalanceSheetViewModel)GetViewModelWithErrorMessage(accSetupBalanceSheetViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Designation by general designation master id.
        public virtual AccSetupBalanceSheetViewModel GetBalanceSheet(int balanceSheetId)
        {
            AccSetupBalanceSheetResponse response = _accSetupBalanceSheetClient.GetBalanceSheet(balanceSheetId);
            return response?.AccSetupBalanceSheetModel.ToViewModel<AccSetupBalanceSheetViewModel>();
        }

        //Update Designation.
        public virtual AccSetupBalanceSheetViewModel UpdateBalanceSheet(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Info);
                AccSetupBalanceSheetResponse response = _accSetupBalanceSheetClient.UpdateBalanceSheet(accSetupBalanceSheetViewModel.ToModel<AccSetupBalanceSheetModel>());
                AccSetupBalanceSheetModel accSetupBalanceSheetModel = response?.AccSetupBalanceSheetModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Info);
                return IsNotNull(accSetupBalanceSheetModel) ? accSetupBalanceSheetModel.ToViewModel<AccSetupBalanceSheetViewModel>() : (AccSetupBalanceSheetViewModel)GetViewModelWithErrorMessage(new AccSetupBalanceSheetViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return (AccSetupBalanceSheetViewModel)GetViewModelWithErrorMessage(accSetupBalanceSheetViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Designation.
        public virtual bool DeleteBalanceSheet(string balanceSheetId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accSetupBalanceSheetClient.DeleteBalanceSheet(new ParameterModel { Ids = balanceSheetId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAccSetupBalanceSheetMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
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
                ColumnName = "Balance Sheet",
                ColumnCode = "AccBalancesheetHeadDesc",
                IsSortable = true,
            }); datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Balance Sheet Type",
                ColumnCode = "AccBalsheetTypeDesc",
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
