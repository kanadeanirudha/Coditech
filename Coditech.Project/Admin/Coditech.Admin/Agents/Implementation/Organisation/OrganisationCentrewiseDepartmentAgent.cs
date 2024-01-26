using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;

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
                filters.Add("CenterCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("GeneralDepartmentMasterId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CenterCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentrewiseDepartmentListResponse response = _organisationCentrewiseDepartmentClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentrewiseDepartmentListModel organisationCentrewiseDepartmentList = new OrganisationCentrewiseDepartmentListModel { OrganisationCentrewiseDepartmentList = response?.OrganisationCentrewiseDepartmentList };
            OrganisationCentrewiseDepartmentListViewModel listViewModel = new OrganisationCentrewiseDepartmentListViewModel();
            listViewModel.OrganisationCentrewiseDepartmentList = organisationCentrewiseDepartmentList?.OrganisationCentrewiseDepartmentList?.ToViewModel<OrganisationCentrewiseDepartmentViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentrewiseDepartmentList.Count, BindColumns());
            return listViewModel;
        }

        //Update Associate UnAssociate Centrewise Department.
        public virtual OrganisationCentrewiseDepartmentViewModel UpdateAssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentViewModel organisationCentrewiseDepartmentViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AssociateUnAssociateCentrewiseDepartment.ToString(), TraceLevel.Info);
                OrganisationCentrewiseDepartmentResponse response = _organisationCentrewiseDepartmentClient.UpdateAssociateUnAssociateCentrewiseDepartment(organisationCentrewiseDepartmentViewModel.ToModel<OrganisationCentrewiseDepartmentModel>());
                OrganisationCentrewiseDepartmentModel organisationCentrewiseDepartmentModel = response?.OrganisationCentrewiseDepartmentModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AssociateUnAssociateCentrewiseDepartment.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(organisationCentrewiseDepartmentModel) ? organisationCentrewiseDepartmentModel.ToViewModel<OrganisationCentrewiseDepartmentViewModel>() : (OrganisationCentrewiseDepartmentViewModel)GetViewModelWithErrorMessage(new OrganisationCentrewiseDepartmentViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociateUnAssociateCentrewiseDepartment.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseDepartmentViewModel)GetViewModelWithErrorMessage(organisationCentrewiseDepartmentViewModel, GeneralResources.UpdateErrorMessage);
            }
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
