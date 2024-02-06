using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Model;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class OrganisationCentrewiseBuildingAgent : BaseAgent, IOrganisationCentrewiseBuildingAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewiseBuildingClient _organisationCentrewiseBuildingClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewiseBuildingAgent(ICoditechLogging coditechLogging, IOrganisationCentrewiseBuildingClient organisationCentrewiseBuildingClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewiseBuildingClient = GetClient<IOrganisationCentrewiseBuildingClient>(organisationCentrewiseBuildingClient);
        }
        #endregion

        #region Public Methods
        public virtual OrganisationCentrewiseBuildingListViewModel GetOrganisationCentrewiseBuildingList(DataTableViewModel dataTableModel, string centreCode)
        {
            FilterCollection filters = new FilterCollection() ;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {                 
                filters.Add("BuildName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            filters.Add("BuildName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            filters.Add(FilterKeys.SelectedDepartmentId, ProcedureFilterOperators.Equals, Convert.ToString(dataTableModel.SelectedDepartmentId));

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CentreName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentrewiseBuildingListResponse response = _organisationCentrewiseBuildingClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentrewiseBuildingListModel organisationCentrewiseBuildingList = new OrganisationCentrewiseBuildingListModel { OrganisationCentrewiseBuildingList = response?.OrganisationCentrewiseBuildingList };
            OrganisationCentrewiseBuildingListViewModel listViewModel = new OrganisationCentrewiseBuildingListViewModel();
            listViewModel.OrganisationCentrewiseBuildingList = organisationCentrewiseBuildingList?.OrganisationCentrewiseBuildingList?.ToViewModel<OrganisationCentrewiseBuildingViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentrewiseBuildingList.Count, BindColumns());
            return listViewModel;
        }

        //Create Organisation Centre.
        public virtual OrganisationCentrewiseBuildingViewModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel)
        {
            try
            {
                OrganisationCentrewiseBuildingResponse response = _organisationCentrewiseBuildingClient.CreateOrganisationCentrewiseBuilding(organisationCentrewiseBuildingViewModel.ToModel<OrganisationCentrewiseBuildingModel>());
                OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel = response?.OrganisationCentrewiseBuildingModel;
                return IsNotNull(organisationCentrewiseBuildingModel) ? organisationCentrewiseBuildingModel.ToViewModel<OrganisationCentrewiseBuildingViewModel>() : new OrganisationCentrewiseBuildingViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentrewiseBuildingViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentrewiseBuildingViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseBuildingViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Organisation Centre by organisationCentreId.
        public virtual OrganisationCentrewiseBuildingViewModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId)
        {
            OrganisationCentrewiseBuildingResponse response = _organisationCentrewiseBuildingClient.GetOrganisationCentrewiseBuilding(organisationCentrewiseBuildingId);
            return response?.OrganisationCentrewiseBuildingModel.ToViewModel<OrganisationCentrewiseBuildingViewModel>();
        }

        //Update Organisation Centre.
        public virtual OrganisationCentrewiseBuildingViewModel UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                OrganisationCentrewiseBuildingResponse response = _organisationCentrewiseBuildingClient.UpdateOrganisationCentrewiseBuilding(organisationCentrewiseBuildingViewModel.ToModel<OrganisationCentrewiseBuildingModel>());
                OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel = response?.OrganisationCentrewiseBuildingModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                return IsNotNull(organisationCentrewiseBuildingModel) ? organisationCentrewiseBuildingModel.ToViewModel<OrganisationCentrewiseBuildingViewModel>() : (OrganisationCentrewiseBuildingViewModel)GetViewModelWithErrorMessage(new OrganisationCentrewiseBuildingViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseBuildingViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Organisation Centre.
        public virtual bool DeleteOrganisationCentrewiseBuilding(string organisationCentrewiseBuildingId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _organisationCentrewiseBuildingClient.DeleteOrganisationCentrewiseBuilding(new ParameterModel { Ids = organisationCentrewiseBuildingId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteOrganisationCentreMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
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
                ColumnName = "Build Name",
                ColumnCode = "BuildName",
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
                ColumnName = "Area",
                ColumnCode = "Area",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
