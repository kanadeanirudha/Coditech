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
    public class InventoryItemGroupAgent : BaseAgent, IInventoryItemGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryItemGroupClient _inventoryItemGroupClient;
        #endregion

        #region Public Constructor
        public InventoryItemGroupAgent(ICoditechLogging coditechLogging, IInventoryItemGroupClient inventoryItemGroupClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryItemGroupClient = GetClient<IInventoryItemGroupClient>(inventoryItemGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryItemGroupListViewModel GetInventoryItemGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ItemGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ItemGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ItemGroupName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryItemGroupListResponse response = _inventoryItemGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryItemGroupListModel inventoryItemGroupList = new InventoryItemGroupListModel { InventoryItemGroupList = response?.InventoryItemGroupList };
            InventoryItemGroupListViewModel listViewModel = new InventoryItemGroupListViewModel();
            listViewModel.InventoryItemGroupList = inventoryItemGroupList?.InventoryItemGroupList?.ToViewModel<InventoryItemGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryItemGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryItemGroup.
        public virtual InventoryItemGroupViewModel CreateInventoryItemGroup(InventoryItemGroupViewModel inventoryItemGroupViewModel)
        {
            try
            {
                InventoryItemGroupResponse response = _inventoryItemGroupClient.CreateInventoryItemGroup(inventoryItemGroupViewModel.ToModel<InventoryItemGroupModel>());
                InventoryItemGroupModel inventoryItemGroupModel = response?.InventoryItemGroupModel;
                return IsNotNull(inventoryItemGroupModel) ? inventoryItemGroupModel.ToViewModel<InventoryItemGroupViewModel>() : new InventoryItemGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryItemGroupViewModel)GetViewModelWithErrorMessage(inventoryItemGroupViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryItemGroupViewModel)GetViewModelWithErrorMessage(inventoryItemGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return (InventoryItemGroupViewModel)GetViewModelWithErrorMessage(inventoryItemGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get InventoryItemGroup by inventory item group id.
        public virtual InventoryItemGroupViewModel GetInventoryItemGroup(short inventoryItemGroupId)
        {
            InventoryItemGroupResponse response = _inventoryItemGroupClient.GetInventoryItemGroup(inventoryItemGroupId);
            return response?.InventoryItemGroupModel.ToViewModel<InventoryItemGroupViewModel>();
        }

        //Update inventoryItemGroup.
        public virtual InventoryItemGroupViewModel UpdateInventoryItemGroup(InventoryItemGroupViewModel inventoryItemGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Info);
                InventoryItemGroupResponse response = _inventoryItemGroupClient.UpdateInventoryItemGroup(inventoryItemGroupViewModel.ToModel<InventoryItemGroupModel>());
                InventoryItemGroupModel inventoryItemGroupModel = response?.InventoryItemGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryItemGroupModel) ? inventoryItemGroupModel.ToViewModel<InventoryItemGroupViewModel>() : (InventoryItemGroupViewModel)GetViewModelWithErrorMessage(new InventoryItemGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return (InventoryItemGroupViewModel)GetViewModelWithErrorMessage(inventoryItemGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete inventoryItemGroup.
        public virtual bool DeleteInventoryItemGroup(string inventoryItemGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryItemGroupClient.DeleteInventoryItemGroup(new ParameterModel { Ids = inventoryItemGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryItemGroupMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "Item Group Name",
                ColumnCode = "ItemGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Item Group Code",
                ColumnCode = "ItemGroupCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Consider In Prod Report",
                ColumnCode = "ConsiderInProdReport",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all inventory item group list from database 
        public virtual InventoryItemGroupListResponse GetInventoryItemGroupList()
        {
            InventoryItemGroupListResponse inventoryItemGroupList = _inventoryItemGroupClient.List(null, null, null, 1, int.MaxValue);
            return inventoryItemGroupList?.InventoryItemGroupList?.Count > 0 ? inventoryItemGroupList : new InventoryItemGroupListResponse();
        }
        #endregion
    }
}
