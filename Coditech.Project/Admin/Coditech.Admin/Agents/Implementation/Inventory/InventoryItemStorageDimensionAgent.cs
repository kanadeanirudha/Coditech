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
using System.Xml.Linq;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class InventoryItemStorageDimensionAgent : BaseAgent, IInventoryItemStorageDimensionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryItemStorageDimensionClient _inventoryItemStorageDimensionClient;
        #endregion

        #region Public Constructor
        public InventoryItemStorageDimensionAgent(ICoditechLogging coditechLogging, IInventoryItemStorageDimensionClient inventoryItemStorageDimensionClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryItemStorageDimensionClient = GetClient<IInventoryItemStorageDimensionClient>(inventoryItemStorageDimensionClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryItemStorageDimensionListViewModel GetInventoryItemStorageDimensionList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ParentInventoryItemStorageDimensionId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CategoryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CategoryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ParentInventoryItemStorageDimensionId" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryItemStorageDimensionListResponse response = _inventoryItemStorageDimensionClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryItemStorageDimensionListModel InventoryItemStorageDimensionList = new InventoryItemStorageDimensionListModel { InventoryItemStorageDimensionList = response?.InventoryItemStorageDimensionList };
            InventoryItemStorageDimensionListViewModel listViewModel = new InventoryItemStorageDimensionListViewModel();
            listViewModel.InventoryItemStorageDimensionList = InventoryItemStorageDimensionList?.InventoryItemStorageDimensionList?.ToViewModel<InventoryItemStorageDimensionViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryItemStorageDimensionList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryItemStorageDimension.
        public virtual InventoryItemStorageDimensionViewModel CreateInventoryItemStorageDimension(InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel)
        {
            try
            {
                InventoryItemStorageDimensionResponse response = _inventoryItemStorageDimensionClient.CreateInventoryItemStorageDimension(inventoryItemStorageDimensionViewModel.ToModel<InventoryItemStorageDimensionModel>());
                InventoryItemStorageDimensionModel inventoryItemStorageDimensionModel = response?.InventoryItemStorageDimensionModel;
                return IsNotNull(inventoryItemStorageDimensionModel) ? inventoryItemStorageDimensionModel.ToViewModel<InventoryItemStorageDimensionViewModel>() : new InventoryItemStorageDimensionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryItemStorageDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemStorageDimensionViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryItemStorageDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemStorageDimensionViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return (InventoryItemStorageDimensionViewModel)GetViewModelWithErrorMessage(inventoryItemStorageDimensionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryItemStorageDimension by general InventoryItemStorageDimension master id.
        public virtual InventoryItemStorageDimensionViewModel GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId)
        {
            InventoryItemStorageDimensionResponse response = _inventoryItemStorageDimensionClient.GetInventoryItemStorageDimension(inventoryItemStorageDimensionId);
            return response?.InventoryItemStorageDimensionModel.ToViewModel<InventoryItemStorageDimensionViewModel>();
        }

        //Update InventoryItemStorageDimension.
        public virtual InventoryItemStorageDimensionViewModel UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionViewModel InventoryItemStorageDimensionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Info);
                InventoryItemStorageDimensionResponse response = _inventoryItemStorageDimensionClient.UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionViewModel.ToModel<InventoryItemStorageDimensionModel>());
                InventoryItemStorageDimensionModel inventoryItemStorageDimensionModel = response?.InventoryItemStorageDimensionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryItemStorageDimensionModel) ? inventoryItemStorageDimensionModel.ToViewModel<InventoryItemStorageDimensionViewModel>() : (InventoryItemStorageDimensionViewModel)GetViewModelWithErrorMessage(new InventoryItemStorageDimensionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return (InventoryItemStorageDimensionViewModel)GetViewModelWithErrorMessage(InventoryItemStorageDimensionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryItemStorageDimension.
        public virtual bool DeleteInventoryItemStorageDimension(string InventoryItemStorageDimensionId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryItemStorageDimensionClient.DeleteInventoryItemStorageDimension(new ParameterModel { Ids = InventoryItemStorageDimensionId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryItemStorageDimension;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
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
                ColumnName = "Category Name",
                ColumnCode = "CategoryName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Category Code",
                ColumnCode = "CategoryCode",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryItemStorageDimension list from database 
        public virtual InventoryItemStorageDimensionListResponse GetInventoryItemStorageDimensionList()
        {
            InventoryItemStorageDimensionListResponse InventoryItemStorageDimensionList = _inventoryItemStorageDimensionClient.List(null, null, null, 1, int.MaxValue);
            return InventoryItemStorageDimensionList?.InventoryItemStorageDimensionList?.Count > 0 ? InventoryItemStorageDimensionList : new InventoryItemStorageDimensionListResponse();
        }
        #endregion
    }
}
