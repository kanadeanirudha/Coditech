using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses.Inventory.InventoryGeneralItemMaster;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class InventoryGeneralItemMasterAgent : BaseAgent, IInventoryGeneralItemMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryGeneralItemMasterClient _inventoryGeneralItemMasterClient;
        #endregion

        #region Public Constructor
        public InventoryGeneralItemMasterAgent(ICoditechLogging coditechLogging, IInventoryGeneralItemMasterClient inventoryGeneralItemMasterClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryGeneralItemMasterClient = GetClient<IInventoryGeneralItemMasterClient>(inventoryGeneralItemMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryGeneralItemMasterListViewModel GetInventoryGeneralItemMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ItemName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ItemNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "InventoryGeneralItemMasterName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryGeneralItemMasterListResponse response = _inventoryGeneralItemMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryGeneralItemMasterListModel inventoryGeneralItemMasterList = new InventoryGeneralItemMasterListModel { InventoryGeneralItemMasterList = response?.InventoryGeneralItemMasterList };
            InventoryGeneralItemMasterListViewModel listViewModel = new InventoryGeneralItemMasterListViewModel();
            listViewModel.InventoryGeneralItemMasterList = inventoryGeneralItemMasterList?.InventoryGeneralItemMasterList?.ToViewModel<InventoryGeneralItemMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryGeneralItemMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryGeneralItemMaster.
        public virtual InventoryGeneralItemMasterViewModel CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel)
        {
            try
            {
                InventoryGeneralItemMasterResponse response = _inventoryGeneralItemMasterClient.CreateInventoryGeneralItemMaster(inventoryGeneralItemMasterViewModel.ToModel<InventoryGeneralItemMasterModel>());
                InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel = response?.InventoryGeneralItemMasterModel;
                return IsNotNull(inventoryGeneralItemMasterModel) ? inventoryGeneralItemMasterModel.ToViewModel<InventoryGeneralItemMasterViewModel>() : new InventoryGeneralItemMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryGeneralItemMasterViewModel)GetViewModelWithErrorMessage(inventoryGeneralItemMasterViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryGeneralItemMasterViewModel)GetViewModelWithErrorMessage(inventoryGeneralItemMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Error);
                return (InventoryGeneralItemMasterViewModel)GetViewModelWithErrorMessage(inventoryGeneralItemMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryGeneralItemMaster by general inventoryGeneralItemMaster master id.
        public virtual InventoryGeneralItemMasterViewModel GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId)
        {
            InventoryGeneralItemMasterResponse response = _inventoryGeneralItemMasterClient.GetInventoryGeneralItemMaster(inventoryGeneralItemMasterId);
            return response?.InventoryGeneralItemMasterModel.ToViewModel<InventoryGeneralItemMasterViewModel>();
        }

        //Update inventoryGeneralItemMaster.
        public virtual InventoryGeneralItemMasterViewModel UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Info);
                InventoryGeneralItemMasterResponse response = _inventoryGeneralItemMasterClient.UpdateInventoryGeneralItemMaster(inventoryGeneralItemMasterViewModel.ToModel<InventoryGeneralItemMasterModel>());
                InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel = response?.InventoryGeneralItemMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryGeneralItemMasterModel) ? inventoryGeneralItemMasterModel.ToViewModel<InventoryGeneralItemMasterViewModel>() : (InventoryGeneralItemMasterViewModel)GetViewModelWithErrorMessage(new InventoryGeneralItemMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Error);
                return (InventoryGeneralItemMasterViewModel)GetViewModelWithErrorMessage(inventoryGeneralItemMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete inventoryGeneralItemMaster.
        public virtual bool DeleteInventoryGeneralItemMaster(string inventoryGeneralItemMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryGeneralItemMasterClient.DeleteInventoryGeneralItemMaster(new ParameterModel { Ids = inventoryGeneralItemMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryGeneralItemMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryGeneralItemMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Item Name",
                ColumnCode = "ItemName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Item Number",
                ColumnCode = "ItemNumber",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all inventoryGeneralItemMaster list from database 
        public virtual InventoryGeneralItemMasterListResponse GetInventoryGeneralItemMasterList()
        {
            InventoryGeneralItemMasterListResponse inventoryGeneralItemMasterList = _inventoryGeneralItemMasterClient.List(null, null, null, 1, int.MaxValue);
            return inventoryGeneralItemMasterList?.InventoryGeneralItemMasterList?.Count > 0 ? inventoryGeneralItemMasterList : new InventoryGeneralItemMasterListResponse();
        }
        #endregion
    }
}
