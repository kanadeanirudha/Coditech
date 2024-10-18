﻿using Coditech.Admin.ViewModel;
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
    public class DBTMTestAgent : BaseAgent, IDBTMTestAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IDBTMTestClient _dBTMTestClient;
        #endregion

        #region Public Constructor
        public DBTMTestAgent(ICoditechLogging coditechLogging, IDBTMTestClient dBTMTestClient)
        {
            _coditechLogging = coditechLogging;
            _dBTMTestClient = GetClient<IDBTMTestClient>(dBTMTestClient);
        }
        #endregion

        #region Public Methods
        public virtual DBTMTestListViewModel GetDBTMTestList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("TestName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TestCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "TestName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            DBTMTestListResponse response = _dBTMTestClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            DBTMTestListModel dBTMTestList = new DBTMTestListModel { DBTMTestList = response?.DBTMTestList };
            DBTMTestListViewModel listViewModel = new DBTMTestListViewModel();
            listViewModel.DBTMTestList = dBTMTestList?.DBTMTestList?.ToViewModel<DBTMTestViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.DBTMTestList.Count, BindColumns());
            return listViewModel;
        }

        //Create DBTMTest.
        public virtual DBTMTestViewModel CreateDBTMTest(DBTMTestViewModel dBTMTestViewModel)
        {
            try
            {
                DBTMTestResponse response = _dBTMTestClient.CreateDBTMTest(dBTMTestViewModel.ToModel<DBTMTestModel>());
                DBTMTestModel dBTMTestModel = response?.DBTMTestModel;
                return IsNotNull(dBTMTestModel) ? dBTMTestModel.ToViewModel<DBTMTestViewModel>() : new DBTMTestViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (DBTMTestViewModel)GetViewModelWithErrorMessage(dBTMTestViewModel, ex.ErrorMessage);
                    default:
                        return (DBTMTestViewModel)GetViewModelWithErrorMessage(dBTMTestViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Error);
                return (DBTMTestViewModel)GetViewModelWithErrorMessage(dBTMTestViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get DBTMTest by dBTMTestMaster id.
        public virtual DBTMTestViewModel GetDBTMTest(int dBTMTestMasterId)
        {
            DBTMTestResponse response = _dBTMTestClient.GetDBTMTest(dBTMTestMasterId);
            return response?.DBTMTestModel.ToViewModel<DBTMTestViewModel>();
        }

        //Update DBTMTest.
        public virtual DBTMTestViewModel UpdateDBTMTest(DBTMTestViewModel dBTMTestViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Info);
                DBTMTestResponse response = _dBTMTestClient.UpdateDBTMTest(dBTMTestViewModel.ToModel<DBTMTestModel>());
                DBTMTestModel dBTMTestModel = response?.DBTMTestModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Info);
                return IsNotNull(dBTMTestModel) ? dBTMTestModel.ToViewModel<DBTMTestViewModel>() : (DBTMTestViewModel)GetViewModelWithErrorMessage(new DBTMTestViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Error);
                return (DBTMTestViewModel)GetViewModelWithErrorMessage(dBTMTestViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete DBTMTest.
        public virtual bool DeleteDBTMTest(string dBTMTestMasterIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _dBTMTestClient.DeleteDBTMTest(new ParameterModel { Ids = dBTMTestMasterIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteDBTMTestMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMTest.ToString(), TraceLevel.Error);
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
                ColumnName = "Test Name",
                ColumnCode = "TestName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Test Code",
                ColumnCode = "TestCode",
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
