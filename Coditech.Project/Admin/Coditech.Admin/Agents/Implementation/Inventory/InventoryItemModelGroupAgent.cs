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
    public class InventoryItemModelGroupAgent : BaseAgent, IInventoryItemModelGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryItemModelGroupClient _inventoryItemModelGroupClient;
        #endregion

        #region Public Constructor
        public InventoryItemModelGroupAgent(ICoditechLogging coditechLogging, IInventoryItemModelGroupClient inventoryItemModelGroupClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryItemModelGroupClient = GetClient<IInventoryItemModelGroupClient>(inventoryItemModelGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryItemModelGroupListViewModel GetInventoryItemModelGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ItemModelGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ItemModelGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ItemModelGroupName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryItemModelGroupListResponse response = _inventoryItemModelGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryItemModelGroupListModel countryList = new InventoryItemModelGroupListModel { InventoryItemModelGroupList = response?.InventoryItemModelGroupList };
            InventoryItemModelGroupListViewModel listViewModel = new InventoryItemModelGroupListViewModel();
            listViewModel.InventoryItemModelGroupList = countryList?.InventoryItemModelGroupList?.ToViewModel<InventoryItemModelGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryItemModelGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryItemModelGroup.
        public virtual InventoryItemModelGroupViewModel CreateInventoryItemModelGroup(InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel)
        {
            try
            {
                InventoryItemModelGroupResponse response = _inventoryItemModelGroupClient.CreateInventoryItemModelGroup(inventoryItemModelGroupViewModel.ToModel<InventoryItemModelGroupModel>());
                InventoryItemModelGroupModel inventoryItemModelGroupModel = response?.InventoryItemModelGroupModel;
                return IsNotNull(inventoryItemModelGroupModel) ? inventoryItemModelGroupModel.ToViewModel<InventoryItemModelGroupViewModel>() : new InventoryItemModelGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryItemModelGroupViewModel)GetViewModelWithErrorMessage(inventoryItemModelGroupViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryItemModelGroupViewModel)GetViewModelWithErrorMessage(inventoryItemModelGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return (InventoryItemModelGroupViewModel)GetViewModelWithErrorMessage(inventoryItemModelGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryItemModelGroup by general country master id.
        public virtual InventoryItemModelGroupViewModel GetInventoryItemModelGroup(short InventoryItemModelGroupId)
        {
            InventoryItemModelGroupResponse response = _inventoryItemModelGroupClient.GetInventoryItemModelGroup(InventoryItemModelGroupId);
            return response?.InventoryItemModelGroupModel.ToViewModel<InventoryItemModelGroupViewModel>();
        }

        //Update InventoryItemModelGroup.
        public virtual InventoryItemModelGroupViewModel UpdateInventoryItemModelGroup(InventoryItemModelGroupViewModel InventoryItemModelGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Info);
                InventoryItemModelGroupResponse response = _inventoryItemModelGroupClient.UpdateInventoryItemModelGroup(InventoryItemModelGroupViewModel.ToModel<InventoryItemModelGroupModel>());
                InventoryItemModelGroupModel InventoryItemModelGroupModel = response?.InventoryItemModelGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Info);
                return IsNotNull(InventoryItemModelGroupModel) ? InventoryItemModelGroupModel.ToViewModel<InventoryItemModelGroupViewModel>() : (InventoryItemModelGroupViewModel)GetViewModelWithErrorMessage(new InventoryItemModelGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return (InventoryItemModelGroupViewModel)GetViewModelWithErrorMessage(InventoryItemModelGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryItemModelGroup.
        public virtual bool DeleteInventoryItemModelGroup(string InventoryItemModelGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryItemModelGroupClient.DeleteInventoryItemModelGroup(new ParameterModel { Ids = InventoryItemModelGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryItemModelGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "Item Model Group Name",
                ColumnCode = "ItemModelGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Item Model Group Code",
                ColumnCode = "ItemModelGroupCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Inventory Model",
                ColumnCode = "InventoryModelEnumId",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryItemModelGroup list from database 
        public virtual InventoryItemModelGroupListResponse GetInventoryItemModelGroupList()
        {
            InventoryItemModelGroupListResponse inventoryItemModelGroupList = _inventoryItemModelGroupClient.List(null, null, null, 1, int.MaxValue);
            return inventoryItemModelGroupList?.InventoryItemModelGroupList?.Count > 0 ? inventoryItemModelGroupList : new InventoryItemModelGroupListResponse();
        }
        #endregion
    }
}
