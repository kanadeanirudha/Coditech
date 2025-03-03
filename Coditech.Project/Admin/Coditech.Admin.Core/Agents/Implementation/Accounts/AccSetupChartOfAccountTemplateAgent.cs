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
    public class AccSetupChartOfAccountTemplateAgent : BaseAgent, IAccSetupChartOfAccountTemplateAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupChartOfAccountTemplateClient _accSetupChartOfAccountTemplateClient;
        #endregion
        #region Public Constructor
        public AccSetupChartOfAccountTemplateAgent(ICoditechLogging coditechLogging, IAccSetupChartOfAccountTemplateClient accSetupChartOfAccountTemplateClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupChartOfAccountTemplateClient = GetClient<IAccSetupChartOfAccountTemplateClient>(accSetupChartOfAccountTemplateClient);
        }
        #endregion
        #region Public Methods
        public virtual AccSetupChartOfAccountTemplateListViewModel GetAccSetupChartOfAccountTemplateList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TemplateName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupChartOfAccountTemplateListResponse response = _accSetupChartOfAccountTemplateClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupChartOfAccountTemplateListModel accSetupChartOfAccountTemplateList = new AccSetupChartOfAccountTemplateListModel { AccSetupChartOfAccountTemplateList = response?.AccSetupChartOfAccountTemplateList };
            AccSetupChartOfAccountTemplateListViewModel listViewModel = new AccSetupChartOfAccountTemplateListViewModel();
            listViewModel.AccSetupChartOfAccountTemplateList = accSetupChartOfAccountTemplateList?.AccSetupChartOfAccountTemplateList?.ToViewModel<AccSetupChartOfAccountTemplateViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupChartOfAccountTemplateList.Count, BindColumns());
            return listViewModel;
        }        
        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Template Name ",
                ColumnCode = "TemplateName",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all AccSetupChartOfAccountTemplate list from database 
        public virtual AccSetupChartOfAccountTemplateListResponse GetAccSetupChartOfAccountTemplateList()
        {
            AccSetupChartOfAccountTemplateListResponse accSetupChartOfAccountTemplateList = _accSetupChartOfAccountTemplateClient.List(null, null, null, 1, int.MaxValue);
            return accSetupChartOfAccountTemplateList?.AccSetupChartOfAccountTemplateList?.Count > 0 ? accSetupChartOfAccountTemplateList : new AccSetupChartOfAccountTemplateListResponse();
        }
        #endregion
    }
}
#endregion