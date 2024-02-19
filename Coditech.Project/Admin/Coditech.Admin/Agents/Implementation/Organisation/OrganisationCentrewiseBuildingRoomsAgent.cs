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

namespace Coditech.Admin.Agents
{
    public class OrganisationCentrewiseBuildingRoomsAgent : BaseAgent, IOrganisationCentrewiseBuildingRoomsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewiseBuildingRoomsClient _organisationCentrewiseBuildingRoomsClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewiseBuildingRoomsAgent(ICoditechLogging coditechLogging, IOrganisationCentrewiseBuildingRoomsClient organisationCentrewiseBuildingRoomsClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewiseBuildingRoomsClient = GetClient<IOrganisationCentrewiseBuildingRoomsClient>(organisationCentrewiseBuildingRoomsClient);
        }
        #endregion

        #region Public Methods
        public virtual OrganisationCentrewiseBuildingRoomsListViewModel GetOrganisationCentrewiseBuildingRoomsList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("RoomName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("BuildingName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "RoomName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentrewiseBuildingRoomsListResponse response = _organisationCentrewiseBuildingRoomsClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentrewiseBuildingRoomsListModel organisationCentrewiseBuildingRoomsList = new OrganisationCentrewiseBuildingRoomsListModel { OrganisationCentrewiseBuildingRoomsList = response?.OrganisationCentrewiseBuildingRoomsList };
            OrganisationCentrewiseBuildingRoomsListViewModel listViewModel = new OrganisationCentrewiseBuildingRoomsListViewModel();
            listViewModel.OrganisationCentrewiseBuildingRoomsList = organisationCentrewiseBuildingRoomsList?.OrganisationCentrewiseBuildingRoomsList?.ToViewModel<OrganisationCentrewiseBuildingRoomsViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentrewiseBuildingRoomsList.Count, BindColumns());
            return listViewModel;
        }

        //Create OrganisationCentrewiseBuildingRooms.
        public virtual OrganisationCentrewiseBuildingRoomsViewModel CreateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel)
        {
            try
            {
                OrganisationCentrewiseBuildingRoomsResponse response = _organisationCentrewiseBuildingRoomsClient.CreateOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomsViewModel.ToModel<OrganisationCentrewiseBuildingRoomsModel>());
                OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel = response?.OrganisationCentrewiseBuildingRoomsModel;
                return HelperUtility.IsNotNull(organisationCentrewiseBuildingRoomsModel) ? organisationCentrewiseBuildingRoomsModel.ToViewModel<OrganisationCentrewiseBuildingRoomsViewModel>() : new OrganisationCentrewiseBuildingRoomsViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentrewiseBuildingRoomsViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingRoomsViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentrewiseBuildingRoomsViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingRoomsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseBuildingRoomsViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingRoomsViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Organisation Centrewise Building Rooms by organisationCentrewiseBuildingRoom id.
        public virtual OrganisationCentrewiseBuildingRoomsViewModel GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId)
        {
            OrganisationCentrewiseBuildingRoomsResponse response = _organisationCentrewiseBuildingRoomsClient.GetOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomId);
            return response?.OrganisationCentrewiseBuildingRoomsModel.ToViewModel<OrganisationCentrewiseBuildingRoomsViewModel>();
        }

        //Update OrganisationCentrewiseBuildingRooms.
        public virtual OrganisationCentrewiseBuildingRoomsViewModel UpdateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Info);
                OrganisationCentrewiseBuildingRoomsResponse response = _organisationCentrewiseBuildingRoomsClient.UpdateOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomsViewModel.ToModel<OrganisationCentrewiseBuildingRoomsModel>());
                OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel = response?.OrganisationCentrewiseBuildingRoomsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(organisationCentrewiseBuildingRoomsModel) ? organisationCentrewiseBuildingRoomsModel.ToViewModel<OrganisationCentrewiseBuildingRoomsViewModel>() : (OrganisationCentrewiseBuildingRoomsViewModel)GetViewModelWithErrorMessage(new OrganisationCentrewiseBuildingRoomsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseBuildingRoomsViewModel)GetViewModelWithErrorMessage(organisationCentrewiseBuildingRoomsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete OrganisationCentrewiseBuildingRooms.
        public virtual bool DeleteOrganisationCentrewiseBuildingRooms(string organisationCentrewiseBuildingRoomId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _organisationCentrewiseBuildingRoomsClient.DeleteOrganisationCentrewiseBuildingRooms(new ParameterModel { Ids = organisationCentrewiseBuildingRoomId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteOrganisationCentrewiseBuildingRooms;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
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
                ColumnName = "Building",
                ColumnCode = "OrganisationCentrewiseBuildingMasterId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Floor",
                ColumnCode = "BuildingFloorEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Room Name",
                ColumnCode = "RoomName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Area sq.ft",
                ColumnCode = "Area",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
