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
    public class InventoryItemTrackingDimensionAgent : BaseAgent, IInventoryItemTrackingDimensionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryItemTrackingDimensionClient _inventoryItemTrackingDimensionClient;
        #endregion

        #region Public Constructor
        public InventoryItemTrackingDimensionAgent(ICoditechLogging coditechLogging, IInventoryItemTrackingDimensionClient inventoryItemTrackingDimensionClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryItemTrackingDimensionClient = GetClient<IInventoryItemTrackingDimensionClient>(inventoryItemTrackingDimensionClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryItemTrackingDimensionListViewModel GetInventoryItemTrackingDimensionList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TrackingDimensionCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TrackingDimensionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "TrackingDimensionName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryItemTrackingDimensionListResponse response = _inventoryItemTrackingDimensionClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryItemTrackingDimensionListModel InventoryItemTrackingDimensionList = new InventoryItemTrackingDimensionListModel { InventoryItemTrackingDimensionList = response?.InventoryItemTrackingDimensionList };
            InventoryItemTrackingDimensionListViewModel listViewModel = new InventoryItemTrackingDimensionListViewModel();
            listViewModel.InventoryItemTrackingDimensionList = InventoryItemTrackingDimensionList?.InventoryItemTrackingDimensionList?.ToViewModel<InventoryItemTrackingDimensionViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryItemTrackingDimensionList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryItemTrackingDimension.
        public virtual InventoryItemTrackingDimensionViewModel CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel)
        {
            try
            {
                InventoryItemTrackingDimensionResponse response = _inventoryItemTrackingDimensionClient.CreateInventoryItemTrackingDimension(inventoryItemTrackingDimensionViewModel.ToModel<InventoryItemTrackingDimensionModel>());
                InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel = response?.InventoryItemTrackingDimensionModel;
                return IsNotNull(inventoryItemTrackingDimensionModel) ? inventoryItemTrackingDimensionModel.ToViewModel<InventoryItemTrackingDimensionViewModel>() : new InventoryItemTrackingDimensionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryItemTrackingDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryItemTrackingDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return (InventoryItemTrackingDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryItemTrackingDimension by general InventoryItemTrackingDimension master id.
        public virtual InventoryItemTrackingDimensionViewModel GetInventoryItemTrackingDimension(short inventoryItemTrackingDimensionId)
        {
            InventoryItemTrackingDimensionResponse response = _inventoryItemTrackingDimensionClient.GetInventoryItemTrackingDimension(inventoryItemTrackingDimensionId);
            return response?.InventoryItemTrackingDimensionModel.ToViewModel<InventoryItemTrackingDimensionViewModel>();
        }

        //Update InventoryItemTrackingDimension.
        public virtual InventoryItemTrackingDimensionViewModel UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionViewModel InventoryItemTrackingDimensionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Info);
                InventoryItemTrackingDimensionResponse response = _inventoryItemTrackingDimensionClient.UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionViewModel.ToModel<InventoryItemTrackingDimensionModel>());
                InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel = response?.InventoryItemTrackingDimensionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryItemTrackingDimensionModel) ? inventoryItemTrackingDimensionModel.ToViewModel<InventoryItemTrackingDimensionViewModel>() : (InventoryItemTrackingDimensionViewModel)GetViewModelWithErrorMessage(new InventoryItemTrackingDimensionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return (InventoryItemTrackingDimensionViewModel)GetViewModelWithErrorMessage(InventoryItemTrackingDimensionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryItemTrackingDimension.
        public virtual bool DeleteInventoryItemTrackingDimension(string InventoryItemTrackingDimensionId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryItemTrackingDimensionClient.DeleteInventoryItemTrackingDimension(new ParameterModel { Ids = InventoryItemTrackingDimensionId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryItemTrackingDimension;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
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
                ColumnName = "Tracking Dimension Name",
                ColumnCode = "TrackingDimensionName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Tracking Dimension Code",
                ColumnCode = "TrackingDimensionCode",
                IsSortable = true,
            });
           
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryItemTrackingDimension list from database 
        public virtual InventoryItemTrackingDimensionListResponse GetInventoryItemTrackingDimensionList()
        {
            InventoryItemTrackingDimensionListResponse InventoryItemTrackingDimensionList = _inventoryItemTrackingDimensionClient.List(null, null, null, 1, int.MaxValue);
            return InventoryItemTrackingDimensionList?.InventoryItemTrackingDimensionList?.Count > 0 ? InventoryItemTrackingDimensionList : new InventoryItemTrackingDimensionListResponse();
        }
        #endregion
    }
}
