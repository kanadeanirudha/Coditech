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
    public class TaskApprovalSettingAgent : BaseAgent, ITaskApprovalSettingAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ITaskApprovalSettingClient _taskApprovalSettingClient;
        #endregion

        #region Public Constructor
        public TaskApprovalSettingAgent(ICoditechLogging coditechLogging, ITaskApprovalSettingClient taskApprovalSettingClient)
        {
            _coditechLogging = coditechLogging;
            _taskApprovalSettingClient = GetClient<ITaskApprovalSettingClient> (taskApprovalSettingClient);
        }
        #endregion

        #region Public Methods
        public virtual TaskApprovalSettingListViewModel GetTaskApprovalSettingList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaskCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaskDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            TaskApprovalSettingListResponse response = _taskApprovalSettingClient.List(dataTableModel.SelectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            TaskApprovalSettingListModel taskApprovalSettingList = new TaskApprovalSettingListModel { TaskApprovalSettingList = response?.TaskApprovalSettingList };
            TaskApprovalSettingListViewModel listViewModel = new TaskApprovalSettingListViewModel();
            listViewModel.TaskApprovalSettingList = taskApprovalSettingList?.TaskApprovalSettingList?.ToViewModel<TaskApprovalSettingViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TaskApprovalSettingList.Count, BindColumns());
            return listViewModel;
        }


        //Get taskMaster by taskMasterId.
        public virtual TaskApprovalSettingViewModel GetTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            TaskApprovalSettingResponse response = _taskApprovalSettingClient.GetTaskApprovalSetting(taskMasterId, centreCode);
            return response?.TaskApprovalSettingModel.ToViewModel<TaskApprovalSettingViewModel>();
        }


        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();           
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Task Code",
                ColumnCode = "TaskCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Task Description",
                ColumnCode = "TaskDescription",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        #endregion
        
    }
}
