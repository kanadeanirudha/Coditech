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
    public class InventoryProductDimensionGroupAgent : BaseAgent, IInventoryProductDimensionGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryProductDimensionGroupClient _inventoryProductDimensionGroupClient;
        #endregion

        #region Public Constructor
        public InventoryProductDimensionGroupAgent(ICoditechLogging coditechLogging, IInventoryProductDimensionGroupClient inventoryProductDimensionGroupClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryProductDimensionGroupClient = GetClient<IInventoryProductDimensionGroupClient>(inventoryProductDimensionGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryProductDimensionGroupListViewModel GetInventoryProductDimensionGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ProductDimensionGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ProductDimensionGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ProductDimensionGroupName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryProductDimensionGroupListResponse response = _inventoryProductDimensionGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryProductDimensionGroupListModel inventoryProductDimensionGroupList = new InventoryProductDimensionGroupListModel { InventoryProductDimensionGroupList = response?.InventoryProductDimensionGroupList };
            InventoryProductDimensionGroupListViewModel listViewModel = new InventoryProductDimensionGroupListViewModel();
            listViewModel.InventoryProductDimensionGroupList = inventoryProductDimensionGroupList?.InventoryProductDimensionGroupList?.ToViewModel<InventoryProductDimensionGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryProductDimensionGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryProductDimensionGroup.
        public virtual InventoryProductDimensionGroupViewModel CreateInventoryProductDimensionGroup(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel)
        {
            try
            {
                InventoryProductDimensionGroupResponse response = _inventoryProductDimensionGroupClient.CreateInventoryProductDimensionGroup(inventoryProductDimensionGroupViewModel.ToModel<InventoryProductDimensionGroupModel>());
                InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel = response?.InventoryProductDimensionGroupModel;
                return IsNotNull(inventoryProductDimensionGroupModel) ? inventoryProductDimensionGroupModel.ToViewModel<InventoryProductDimensionGroupViewModel>() : new InventoryProductDimensionGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryProductDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionGroupViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryProductDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryProductDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get  InventoryProductDimensionGroup by  inventoryProductDimensionGroup  id.
        public virtual InventoryProductDimensionGroupViewModel GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId)
        {
            InventoryProductDimensionGroupResponse response = _inventoryProductDimensionGroupClient.GetInventoryProductDimensionGroup(inventoryProductDimensionGroupId);
            return response?.InventoryProductDimensionGroupModel.ToViewModel<InventoryProductDimensionGroupViewModel>();
        }

        //Update inventoryProductDimensionGroup.
        public virtual InventoryProductDimensionGroupViewModel UpdateInventoryProductDimensionGroup(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Info);
                InventoryProductDimensionGroupResponse response = _inventoryProductDimensionGroupClient.UpdateInventoryProductDimensionGroup(inventoryProductDimensionGroupViewModel.ToModel<InventoryProductDimensionGroupModel>());
                InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel = response?.InventoryProductDimensionGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryProductDimensionGroupModel) ? inventoryProductDimensionGroupModel.ToViewModel<InventoryProductDimensionGroupViewModel>() : (InventoryProductDimensionGroupViewModel)GetViewModelWithErrorMessage(new InventoryProductDimensionGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return (InventoryProductDimensionGroupViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete inventoryProductDimensionGroup.
        public virtual bool DeleteInventoryProductDimensionGroup(string inventoryProductDimensionGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryProductDimensionGroupClient.DeleteInventoryProductDimensionGroup(new ParameterModel { Ids = inventoryProductDimensionGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryProductDimensionGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "Product Dimension Group Name",
                ColumnCode = "ProductDimensionGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Product Dimension Group Code",
                ColumnCode = "ProductDimensionGroupCode",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all inventoryProductDimensionGroup list from database 
        public virtual InventoryProductDimensionGroupListResponse GetInventoryProductDimensionGroupList()
        {
            InventoryProductDimensionGroupListResponse inventoryProductDimensionGroupList = _inventoryProductDimensionGroupClient.List(null, null, null, 1, int.MaxValue);
            return inventoryProductDimensionGroupList?.InventoryProductDimensionGroupList?.Count > 0 ? inventoryProductDimensionGroupList : new InventoryProductDimensionGroupListResponse();
        }
        #endregion
    }
}
