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
    public class InventoryStorageDimensionGroupAgent : BaseAgent, IInventoryStorageDimensionGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryStorageDimensionGroupClient _inventoryStorageDimensionGroupClient;
        #endregion

        #region Public Constructor
        public InventoryStorageDimensionGroupAgent(ICoditechLogging coditechLogging, IInventoryStorageDimensionGroupClient inventoryStorageDimensionGroupClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryStorageDimensionGroupClient = GetClient<IInventoryStorageDimensionGroupClient>(inventoryStorageDimensionGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryStorageDimensionGroupListViewModel GetInventoryStorageDimensionGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("StorageDimensionGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("StorageDimensionGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryStorageDimensionGroupListResponse response = _inventoryStorageDimensionGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryStorageDimensionGroupListModel inventoryStorageDimensionGroupList = new InventoryStorageDimensionGroupListModel { InventoryStorageDimensionGroupList = response?.InventoryStorageDimensionGroupList };
            InventoryStorageDimensionGroupListViewModel listViewModel = new InventoryStorageDimensionGroupListViewModel();
            listViewModel.InventoryStorageDimensionGroupList = inventoryStorageDimensionGroupList?.InventoryStorageDimensionGroupList?.ToViewModel<InventoryStorageDimensionGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryStorageDimensionGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryStorageDimensionGroup.
        public virtual InventoryStorageDimensionGroupViewModel CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(inventoryStorageDimensionGroupViewModel.StorageDimensionGroupMapperData))
                {
                    List<InventoryStorageDimensionGroupMapperModel> inventoryStorageDimensionGroupMapperList = JsonConvert.DeserializeObject<List<InventoryStorageDimensionGroupMapperModel>>(inventoryStorageDimensionGroupViewModel.StorageDimensionGroupMapperData);
                    inventoryStorageDimensionGroupViewModel.InventoryStorageDimensionGroupMapperList = inventoryStorageDimensionGroupMapperList;
                }
                InventoryStorageDimensionGroupResponse response = _inventoryStorageDimensionGroupClient.CreateInventoryStorageDimensionGroup(inventoryStorageDimensionGroupViewModel.ToModel<InventoryStorageDimensionGroupModel>());
                InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel = response?.InventoryStorageDimensionGroupModel;
                return IsNotNull(inventoryStorageDimensionGroupModel) ? inventoryStorageDimensionGroupModel.ToViewModel<InventoryStorageDimensionGroupViewModel>() : new InventoryStorageDimensionGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryStorageDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryStorageDimensionGroupViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryStorageDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryStorageDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryStorageDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryStorageDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get  InventoryStorageDimensionGroup by  inventoryStorageDimensionGroup  id.
        public virtual InventoryStorageDimensionGroupViewModel GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId)
        {
            InventoryStorageDimensionGroupResponse response = _inventoryStorageDimensionGroupClient.GetInventoryStorageDimensionGroup(inventoryStorageDimensionGroupId);
            return response?.InventoryStorageDimensionGroupModel.ToViewModel<InventoryStorageDimensionGroupViewModel>();
        }

        //Update inventoryStorageDimensionGroup.
        public virtual InventoryStorageDimensionGroupViewModel UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Info);
                if (!string.IsNullOrEmpty(inventoryStorageDimensionGroupViewModel.StorageDimensionGroupMapperData))
                {
                    List<InventoryStorageDimensionGroupMapperModel> inventoryStorageDimensionGroupMapperList = JsonConvert.DeserializeObject<List<InventoryStorageDimensionGroupMapperModel>>(inventoryStorageDimensionGroupViewModel.StorageDimensionGroupMapperData);
                    inventoryStorageDimensionGroupViewModel.InventoryStorageDimensionGroupMapperList = inventoryStorageDimensionGroupMapperList;
                }
                InventoryStorageDimensionGroupResponse response = _inventoryStorageDimensionGroupClient.UpdateInventoryStorageDimensionGroup(inventoryStorageDimensionGroupViewModel.ToModel<InventoryStorageDimensionGroupModel>());
                InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel = response?.InventoryStorageDimensionGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryStorageDimensionGroupModel) ? inventoryStorageDimensionGroupModel.ToViewModel<InventoryStorageDimensionGroupViewModel>() : (InventoryStorageDimensionGroupViewModel)GetViewModelWithErrorMessage(new InventoryStorageDimensionGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryStorageDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryStorageDimensionGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete inventoryStorageDimensionGroup.
        public virtual bool DeleteInventoryStorageDimensionGroup(string inventoryStorageDimensionGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryStorageDimensionGroupClient.DeleteInventoryStorageDimensionGroup(new ParameterModel { Ids = inventoryStorageDimensionGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryStorageDimensionGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "Storage Dimension Group Name",
                ColumnCode = "StorageDimensionGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Storage Dimension Group Code",
                ColumnCode = "StorageDimensionGroupCode",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all inventoryStorageDimensionGroup list from database 
        public virtual InventoryStorageDimensionGroupListResponse GetInventoryStorageDimensionGroupList()
        {
            InventoryStorageDimensionGroupListResponse inventoryStorageDimensionGroupList = _inventoryStorageDimensionGroupClient.List(null, null, null, 1, int.MaxValue);
            return inventoryStorageDimensionGroupList?.InventoryStorageDimensionGroupList?.Count > 0 ? inventoryStorageDimensionGroupList : new InventoryStorageDimensionGroupListResponse();
        }
        #endregion
    }
}
