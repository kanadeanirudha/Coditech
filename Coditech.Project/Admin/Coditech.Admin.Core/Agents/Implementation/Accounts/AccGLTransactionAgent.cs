﻿using Coditech.Admin.ViewModel;
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
    public class AccGLTransactionAgent : BaseAgent, IAccGLTransactionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccGLTransactionClient _accGLTransactionClient;
        #endregion

        #region Public Constructor
        public AccGLTransactionAgent(ICoditechLogging coditechLogging, IAccGLTransactionClient accGLTransactionClient)
        {
            _coditechLogging = coditechLogging;
            _accGLTransactionClient = GetClient<IAccGLTransactionClient>(accGLTransactionClient);
        }
        #endregion

        #region Public Methods
        public virtual AccGLTransactionListViewModel GetGLTransactionList(DataTableViewModel dataTableModel, string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId , byte accSetupBalanceSheetTypeId)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                
                //filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                //filters.Add("ShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccGLTransactionListResponse response = _accGLTransactionClient.List(selectedCentreCode, accSetupBalanceSheetId, generalFinancialYearId,accSetupTransactionTypeId, accSetupBalanceSheetTypeId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccGLTransactionListModel accGLTransactionList = new AccGLTransactionListModel { AccGLTransactionList = response?.AccGLTransactionList };
            AccGLTransactionListViewModel listViewModel = new AccGLTransactionListViewModel();
            listViewModel.AccGLTransactionList = accGLTransactionList?.AccGLTransactionList?.ToViewModel<AccGLTransactionViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccGLTransactionList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Designation.
        public virtual AccGLTransactionViewModel CreateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel)
        {
            try
            {
                AccGLTransactionResponse response = _accGLTransactionClient.CreateGLTransaction(accGLTransactionViewModel.ToModel<AccGLTransactionModel>());
                AccGLTransactionModel accGLTransactionModel = response?.AccGLTransactionModel;
                return IsNotNull(accGLTransactionModel) ? accGLTransactionModel.ToViewModel<AccGLTransactionViewModel>() : new AccGLTransactionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel, ex.ErrorMessage);
                    default:
                        return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Designation by general designation master id.
        public virtual AccGLTransactionViewModel GetGLTransaction(long accGLTransactionId)
        {
            AccGLTransactionResponse response = _accGLTransactionClient.GetGLTransaction(accGLTransactionId);
            return response?.AccGLTransactionModel.ToViewModel<AccGLTransactionViewModel>();
        }

        //Update Designation.
        public virtual AccGLTransactionViewModel UpdateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Info);
                AccGLTransactionResponse response = _accGLTransactionClient.UpdateGLTransaction(accGLTransactionViewModel.ToModel<AccGLTransactionModel>());
                AccGLTransactionModel accGLTransactionModel = response?.AccGLTransactionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Info);
                return IsNotNull(accGLTransactionModel) ? accGLTransactionModel.ToViewModel<AccGLTransactionViewModel>() : (AccGLTransactionViewModel)GetViewModelWithErrorMessage(new AccGLTransactionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        ////Delete Designation.
        //public virtual bool DeleteBalanceSheet(string accGLTransactionId, out string errorMessage)
        //{
        //    errorMessage = GeneralResources.ErrorFailedToDelete;

        //    try
        //    {
        //        _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Info);
        //        TrueFalseResponse trueFalseResponse = _accGLTransactionClient.DeleteBalanceSheet(new ParameterModel { Ids = accGLTransactionId });
        //        return trueFalseResponse.IsSuccess;
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AssociationDeleteError:
        //                errorMessage = AdminResources.ErrorDeleteAccGLTransactionMaster;
        //                return false;
        //            default:
        //                errorMessage = GeneralResources.ErrorFailedToDelete;
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
        //        errorMessage = GeneralResources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Balance Sheet",
            //    ColumnCode = "AccBalancesheetHeadDesc",
            //    IsSortable = true,
            //}); datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Balance Type",
            //    ColumnCode = "AccBalsheetTypeDesc",
            //    IsSortable = true,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Short Code",
            //    ColumnCode = "ShortCode",
            //    IsSortable = true,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Designation Level",
            //    ColumnCode = "DesignationLevel",
            //    IsSortable = true,
            //});
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Transaction Date",
                ColumnCode = "TransactionDate",
                IsSortable = true,
            });

            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Narration Description",
                ColumnCode = "NarrationDescription",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
