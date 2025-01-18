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
    public class InventoryCategoryAgent : BaseAgent, IInventoryCategoryAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryCategoryClient _inventoryCategoryClient;
        #endregion

        #region Public Constructor
        public InventoryCategoryAgent(ICoditechLogging coditechLogging, IInventoryCategoryClient inventoryCategoryClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryCategoryClient = GetClient<IInventoryCategoryClient>(inventoryCategoryClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryCategoryListViewModel GetInventoryCategoryList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ParentInventoryCategoryId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CategoryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CategoryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add(" ItemPrefix", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ParentInventoryCategoryId" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryCategoryListResponse response = _inventoryCategoryClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryCategoryListModel InventoryCategoryList = new InventoryCategoryListModel { InventoryCategoryList = response?.InventoryCategoryList };
            InventoryCategoryListViewModel listViewModel = new InventoryCategoryListViewModel();
            listViewModel.InventoryCategoryList = InventoryCategoryList?.InventoryCategoryList?.ToViewModel<InventoryCategoryViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryCategoryList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryCategory.
        public virtual InventoryCategoryViewModel CreateInventoryCategory(InventoryCategoryViewModel inventoryCategoryViewModel)
        {
            try
            {
                InventoryCategoryResponse response = _inventoryCategoryClient.CreateInventoryCategory(inventoryCategoryViewModel.ToModel<InventoryCategoryModel>());
                InventoryCategoryModel inventoryCategoryModel = response?.InventoryCategoryModel;
                return IsNotNull(inventoryCategoryModel) ? inventoryCategoryModel.ToViewModel<InventoryCategoryViewModel>() : new InventoryCategoryViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryCategoryViewModel)GetViewModelWithErrorMessage(inventoryCategoryViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryCategoryViewModel)GetViewModelWithErrorMessage(inventoryCategoryViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return (InventoryCategoryViewModel)GetViewModelWithErrorMessage(inventoryCategoryViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryCategory by general InventoryCategory master id.
        public virtual InventoryCategoryViewModel GetInventoryCategory(short inventoryCategoryId)
        {
            InventoryCategoryResponse response = _inventoryCategoryClient.GetInventoryCategory(inventoryCategoryId);
            return response?.InventoryCategoryModel.ToViewModel<InventoryCategoryViewModel>();
        }

        //Update InventoryCategory.
        public virtual InventoryCategoryViewModel UpdateInventoryCategory(InventoryCategoryViewModel InventoryCategoryViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Info);
                InventoryCategoryResponse response = _inventoryCategoryClient.UpdateInventoryCategory(InventoryCategoryViewModel.ToModel<InventoryCategoryModel>());
                InventoryCategoryModel inventoryCategoryModel = response?.InventoryCategoryModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryCategoryModel) ? inventoryCategoryModel.ToViewModel<InventoryCategoryViewModel>() : (InventoryCategoryViewModel)GetViewModelWithErrorMessage(new InventoryCategoryViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return (InventoryCategoryViewModel)GetViewModelWithErrorMessage(InventoryCategoryViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryCategory.
        public virtual bool DeleteInventoryCategory(string InventoryCategoryId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryCategoryClient.DeleteInventoryCategory(new ParameterModel { Ids = InventoryCategoryId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryCategory;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
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
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Item Prefix",
                ColumnCode = "ItemPrefix",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryCategory list from database 
        public virtual InventoryCategoryListResponse GetInventoryCategoryList()
        {
            InventoryCategoryListResponse InventoryCategoryList = _inventoryCategoryClient.List(null, null, null, 1, int.MaxValue);
            return InventoryCategoryList?.InventoryCategoryList?.Count > 0 ? InventoryCategoryList : new InventoryCategoryListResponse();
        }
        #endregion
    }
}
