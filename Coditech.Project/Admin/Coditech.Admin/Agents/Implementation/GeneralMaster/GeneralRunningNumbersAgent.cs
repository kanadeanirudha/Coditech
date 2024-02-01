using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

namespace Coditech.Admin.Agents
{
    public class GeneralRunningNumbersAgent : BaseAgent, IGeneralRunningNumbersAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralRunningNumbersClient _generalRunningNumbersClient;
        #endregion

        #region Public Constructor
        public GeneralRunningNumbersAgent(ICoditechLogging coditechLogging, IGeneralRunningNumbersClient generalRunningNumbersClient)
        {
            _coditechLogging = coditechLogging;
            _generalRunningNumbersClient = GetClient<IGeneralRunningNumbersClient>(generalRunningNumbersClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralRunningNumbersListViewModel GetRunningNumbersList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("IsActive", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "Description" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralRunningNumbersListResponse response = _generalRunningNumbersClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralRunningNumbersListModel organisationCentrewiseDepartmentList = new GeneralRunningNumbersListModel { GeneralRunningNumbersList = response?.GeneralRunningNumbersList };
            GeneralRunningNumbersListViewModel listViewModel = new GeneralRunningNumbersListViewModel();
            listViewModel.GeneralRunningNumbersList = organisationCentrewiseDepartmentList?.GeneralRunningNumbersList?.ToViewModel<GeneralRunningNumbersViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralRunningNumbersList.Count, BindColumns());
            return listViewModel;
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Description",
                ColumnCode = "Description",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Display Format",
                ColumnCode = "DisplayFormat",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Start Sequence",
                ColumnCode = "StartSequence",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Current Sequnce",
                ColumnCode = "CurrentSequnce",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Sequence Reset",
                ColumnCode = "IsSequenceReset",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Separator Char",
                ColumnCode = "SeparatorChar",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Prefix",
                ColumnCode = "Prefix",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Back Dated Prefix",
                ColumnCode = "BackDatedPrefix",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
