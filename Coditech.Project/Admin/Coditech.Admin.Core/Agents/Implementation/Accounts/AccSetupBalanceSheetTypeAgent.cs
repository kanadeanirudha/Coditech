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
    public class AccSetupBalanceSheetTypeAgent : BaseAgent, IAccSetupBalanceSheetTypeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupBalanceSheetTypeClient _accSetupBalanceSheetTypeClient;
        #endregion
        #region Public Constructor
        public AccSetupBalanceSheetTypeAgent(ICoditechLogging coditechLogging, IAccSetupBalanceSheetTypeClient accSetupBalanceSheetTypeClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupBalanceSheetTypeClient = GetClient<IAccSetupBalanceSheetTypeClient>(accSetupBalanceSheetTypeClient);
        }
        #endregion
        #region Public Methods
        public virtual AccSetupBalanceSheetTypeListViewModel GetAccSetupBalanceSheetTypeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("AccBalsheetTypeCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("AccBalsheetTypeDesc", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupBalanceSheetTypeListResponse response = _accSetupBalanceSheetTypeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupBalanceSheetTypeListModel accSetupBalanceSheetTypeList = new AccSetupBalanceSheetTypeListModel { AccSetupBalanceSheetTypeList = response?.AccSetupBalanceSheetTypeList };
            AccSetupBalanceSheetTypeListViewModel listViewModel = new AccSetupBalanceSheetTypeListViewModel();
            listViewModel.AccSetupBalanceSheetTypeList = accSetupBalanceSheetTypeList?.AccSetupBalanceSheetTypeList?.ToViewModel<AccSetupBalanceSheetTypeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupBalanceSheetTypeList.Count, BindColumns());
            return listViewModel;
        }        
        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Balance Type ",
                ColumnCode = "AccBalsheetTypeCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Type Description",
                ColumnCode = "AccBalsheetTypeDesc",
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
        #region
        // it will return get all AccSetupBalanceSheetType list from database 
        public virtual AccSetupBalanceSheetTypeListResponse GetAccSetupBalanceSheetTypeList()
        {
            AccSetupBalanceSheetTypeListResponse accSetupBalanceSheetTypeList = _accSetupBalanceSheetTypeClient.List(null, null, null, 1, int.MaxValue);
            return accSetupBalanceSheetTypeList?.AccSetupBalanceSheetTypeList?.Count > 0 ? accSetupBalanceSheetTypeList : new AccSetupBalanceSheetTypeListResponse();
        }
        #endregion
    }
}
#endregion