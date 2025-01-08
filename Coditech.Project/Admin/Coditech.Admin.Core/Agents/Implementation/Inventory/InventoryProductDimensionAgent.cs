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
    public class InventoryProductDimensionAgent : BaseAgent, IInventoryProductDimensionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryProductDimensionClient _inventoryProductDimensionClient;
        #endregion

        #region Public Constructor
        public InventoryProductDimensionAgent(ICoditechLogging coditechLogging, IInventoryProductDimensionClient inventoryProductDimensionClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryProductDimensionClient = GetClient<IInventoryProductDimensionClient>(inventoryProductDimensionClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryProductDimensionListViewModel GetInventoryProductDimensionList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ProductDimensionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ProductDimensionCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ProductDimensionName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryProductDimensionListResponse response = _inventoryProductDimensionClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryProductDimensionListModel InventoryProductDimensionList = new InventoryProductDimensionListModel { InventoryProductDimensionList = response?.InventoryProductDimensionList };
            InventoryProductDimensionListViewModel listViewModel = new InventoryProductDimensionListViewModel();
            listViewModel.InventoryProductDimensionList = InventoryProductDimensionList?.InventoryProductDimensionList?.ToViewModel<InventoryProductDimensionViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryProductDimensionList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryProductDimension.
        public virtual InventoryProductDimensionViewModel CreateInventoryProductDimension(InventoryProductDimensionViewModel inventoryProductDimensionViewModel)
        {
            try
            {
                InventoryProductDimensionResponse response = _inventoryProductDimensionClient.CreateInventoryProductDimension(inventoryProductDimensionViewModel.ToModel<InventoryProductDimensionModel>());
                InventoryProductDimensionModel inventoryProductDimensionModel = response?.InventoryProductDimensionModel;
                return IsNotNull(inventoryProductDimensionModel) ? inventoryProductDimensionModel.ToViewModel<InventoryProductDimensionViewModel>() : new InventoryProductDimensionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryProductDimensionViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryProductDimensionViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return (InventoryProductDimensionViewModel)GetViewModelWithErrorMessage(inventoryProductDimensionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryProductDimension by general InventoryProductDimension master id.
        public virtual InventoryProductDimensionViewModel GetInventoryProductDimension(short inventoryProductDimensionId)
        {
            InventoryProductDimensionResponse response = _inventoryProductDimensionClient.GetInventoryProductDimension(inventoryProductDimensionId);
            return response?.InventoryProductDimensionModel.ToViewModel<InventoryProductDimensionViewModel>();
        }

        //Update InventoryProductDimension.
        public virtual InventoryProductDimensionViewModel UpdateInventoryProductDimension(InventoryProductDimensionViewModel InventoryProductDimensionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Info);
                InventoryProductDimensionResponse response = _inventoryProductDimensionClient.UpdateInventoryProductDimension(InventoryProductDimensionViewModel.ToModel<InventoryProductDimensionModel>());
                InventoryProductDimensionModel inventoryProductDimensionModel = response?.InventoryProductDimensionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryProductDimensionModel) ? inventoryProductDimensionModel.ToViewModel<InventoryProductDimensionViewModel>() : (InventoryProductDimensionViewModel)GetViewModelWithErrorMessage(new InventoryProductDimensionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return (InventoryProductDimensionViewModel)GetViewModelWithErrorMessage(InventoryProductDimensionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryProductDimension.
        public virtual bool DeleteInventoryProductDimension(string InventoryProductDimensionId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryProductDimensionClient.DeleteInventoryProductDimension(new ParameterModel { Ids = InventoryProductDimensionId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryProductDimension;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
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
                ColumnName = "Product Dimension Name",
                ColumnCode = "ProductDimensionName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Product Dimension Code",
                ColumnCode = "ProductDimensionCode",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryProductDimension list from database 
        public virtual InventoryProductDimensionListResponse GetInventoryProductDimensionList()
        {
            InventoryProductDimensionListResponse InventoryProductDimensionList = _inventoryProductDimensionClient.List(null, null, null, 1, int.MaxValue);
            return InventoryProductDimensionList?.InventoryProductDimensionList?.Count > 0 ? InventoryProductDimensionList : new InventoryProductDimensionListResponse();
        }
        #endregion
    }
}
