using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

namespace Coditech.Admin.Agents
{
    public class OrganisationCentrewiseDepartmentAgent : BaseAgent, IOrganisationCentrewiseDepartmentAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewiseDepartmentClient _organisationCentrewiseDepartmentClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewiseDepartmentAgent(ICoditechLogging coditechLogging, IOrganisationCentrewiseDepartmentClient organisationCentrewiseDepartmentClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewiseDepartmentClient = GetClient<IOrganisationCentrewiseDepartmentClient>(organisationCentrewiseDepartmentClient);
        }
        #endregion

        #region Public Methods
        public virtual OrganisationCentrewiseDepartmentListViewModel GetOrganisationCentrewiseDepartmentList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DepartmentSeqNo", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CentreCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentrewiseDepartmentListResponse response = _organisationCentrewiseDepartmentClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentrewiseDepartmentListModel organisationCentrewiseDepartmentList = new OrganisationCentrewiseDepartmentListModel { OrganisationCentrewiseDepartmentList = response?.OrganisationCentrewiseDepartmentList };
            OrganisationCentrewiseDepartmentListViewModel listViewModel = new OrganisationCentrewiseDepartmentListViewModel();
            listViewModel.OrganisationCentrewiseDepartmentList = organisationCentrewiseDepartmentList?.OrganisationCentrewiseDepartmentList?.ToViewModel<OrganisationCentrewiseDepartmentViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentrewiseDepartmentList.Count, BindColumns());
            return listViewModel;
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Department",
                ColumnCode = "GeneralDepartmentMasterId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Centre Code",
                ColumnCode = "CentreCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Active Flag",
                ColumnCode = "ActiveFlag",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
