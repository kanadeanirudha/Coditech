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

using Newtonsoft.Json;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class InventoryItemTrackingDimensionGroupAgent : BaseAgent, IInventoryItemTrackingDimensionGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryItemTrackingDimensionGroupClient _inventoryItemTrackingDimensionGroupClient;
        #endregion

        #region Public Constructor
        public InventoryItemTrackingDimensionGroupAgent(ICoditechLogging coditechLogging, IInventoryItemTrackingDimensionGroupClient inventoryItemTrackingDimensionGroupClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryItemTrackingDimensionGroupClient = GetClient<IInventoryItemTrackingDimensionGroupClient>(inventoryItemTrackingDimensionGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryItemTrackingDimensionGroupListViewModel GetInventoryItemTrackingDimensionGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ItemTrackingDimensionGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ItemTrackingDimensionGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty: dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryItemTrackingDimensionGroupListResponse response = _inventoryItemTrackingDimensionGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryItemTrackingDimensionGroupListModel inventoryItemTrackingDimensionGroupList = new InventoryItemTrackingDimensionGroupListModel { InventoryItemTrackingDimensionGroupList = response?.InventoryItemTrackingDimensionGroupList };
            InventoryItemTrackingDimensionGroupListViewModel listViewModel = new InventoryItemTrackingDimensionGroupListViewModel();
            listViewModel.InventoryItemTrackingDimensionGroupList = inventoryItemTrackingDimensionGroupList?.InventoryItemTrackingDimensionGroupList?.ToViewModel<InventoryItemTrackingDimensionGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryItemTrackingDimensionGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryItemTrackingDimensionGroup.
        public virtual InventoryItemTrackingDimensionGroupViewModel CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(inventoryItemTrackingDimensionGroupViewModel.ItemTrackingDimensionGroupMapperData))
                {
                    List<InventoryItemTrackingDimensionGroupMapperModel> inventoryItemTrackingDimensionGroupMapperList = JsonConvert.DeserializeObject<List<InventoryItemTrackingDimensionGroupMapperModel>>(inventoryItemTrackingDimensionGroupViewModel.ItemTrackingDimensionGroupMapperData);
                    inventoryItemTrackingDimensionGroupViewModel.InventoryItemTrackingDimensionGroupMapperList = inventoryItemTrackingDimensionGroupMapperList;
                }
                InventoryItemTrackingDimensionGroupResponse response = _inventoryItemTrackingDimensionGroupClient.CreateInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupViewModel.ToModel<InventoryItemTrackingDimensionGroupModel>());
                InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel = response?.InventoryItemTrackingDimensionGroupModel;
                return IsNotNull(inventoryItemTrackingDimensionGroupModel) ? inventoryItemTrackingDimensionGroupModel.ToViewModel<InventoryItemTrackingDimensionGroupViewModel>() : new InventoryItemTrackingDimensionGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryItemTrackingDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionGroupViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryItemTrackingDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryItemTrackingDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get  InventoryItemTrackingDimensionGroup by  inventoryItemTrackingDimensionGroup  id.
        public virtual InventoryItemTrackingDimensionGroupViewModel GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId)
        {
            InventoryItemTrackingDimensionGroupResponse response = _inventoryItemTrackingDimensionGroupClient.GetInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupId);
            return response?.InventoryItemTrackingDimensionGroupModel.ToViewModel<InventoryItemTrackingDimensionGroupViewModel>();
        }

        //Update inventoryItemTrackingDimensionGroup.
        public virtual InventoryItemTrackingDimensionGroupViewModel UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Info);
                if (!string.IsNullOrEmpty(inventoryItemTrackingDimensionGroupViewModel.ItemTrackingDimensionGroupMapperData))
                {
                    List<InventoryItemTrackingDimensionGroupMapperModel> inventoryItemTrackingDimensionGroupMapperList = JsonConvert.DeserializeObject<List<InventoryItemTrackingDimensionGroupMapperModel>>(inventoryItemTrackingDimensionGroupViewModel.ItemTrackingDimensionGroupMapperData);
                    inventoryItemTrackingDimensionGroupViewModel.InventoryItemTrackingDimensionGroupMapperList = inventoryItemTrackingDimensionGroupMapperList;
                }
                InventoryItemTrackingDimensionGroupResponse response = _inventoryItemTrackingDimensionGroupClient.UpdateInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupViewModel.ToModel<InventoryItemTrackingDimensionGroupModel>());
                InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel = response?.InventoryItemTrackingDimensionGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryItemTrackingDimensionGroupModel) ? inventoryItemTrackingDimensionGroupModel.ToViewModel<InventoryItemTrackingDimensionGroupViewModel>() : (InventoryItemTrackingDimensionGroupViewModel)GetViewModelWithErrorMessage(new InventoryItemTrackingDimensionGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryItemTrackingDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryItemTrackingDimensionGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete inventoryItemTrackingDimensionGroup.
        public virtual bool DeleteInventoryItemTrackingDimensionGroup(string inventoryItemTrackingDimensionGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryItemTrackingDimensionGroupClient.DeleteInventoryItemTrackingDimensionGroup(new ParameterModel { Ids = inventoryItemTrackingDimensionGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryItemTrackingDimensionGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "ItemTracking Dimension Group Name",
                ColumnCode = "ItemTrackingDimensionGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "ItemTracking Dimension Group Code",
                ColumnCode = "ItemTrackingDimensionGroupCode",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all inventoryItemTrackingDimensionGroup list from database 
        public virtual InventoryItemTrackingDimensionGroupListResponse GetInventoryItemTrackingDimensionGroupList()
        {
            InventoryItemTrackingDimensionGroupListResponse inventoryItemTrackingDimensionGroupList = _inventoryItemTrackingDimensionGroupClient.List(null, null, null, 1, int.MaxValue);
            return inventoryItemTrackingDimensionGroupList?.InventoryItemTrackingDimensionGroupList?.Count > 0 ? inventoryItemTrackingDimensionGroupList : new InventoryItemTrackingDimensionGroupListResponse();
        }
        #endregion
    }
}
