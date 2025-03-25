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
    public class InventoryCategoryTypeAgent : BaseAgent, IInventoryCategoryTypeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryCategoryTypeClient _inventoryCategoryTypeClient;
        #endregion

        #region Public Constructor
        public InventoryCategoryTypeAgent(ICoditechLogging coditechLogging, IInventoryCategoryTypeClient inventoryCategoryTypeClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryCategoryTypeClient = GetClient<IInventoryCategoryTypeClient>(inventoryCategoryTypeClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryCategoryTypeListViewModel GetInventoryCategoryTypeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("CategoryTypeName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? " " : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryCategoryTypeListResponse response = _inventoryCategoryTypeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryCategoryTypeListModel InventoryCategoryList = new InventoryCategoryTypeListModel { InventoryCategoryTypeList = response?.InventoryCategoryTypeList };
            InventoryCategoryTypeListViewModel listViewModel = new InventoryCategoryTypeListViewModel();
            listViewModel.InventoryCategoryTypeList = InventoryCategoryList?.InventoryCategoryTypeList?.ToViewModel<InventoryCategoryTypeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryCategoryTypeList.Count, BindColumns());
            return listViewModel;
        }

        //Create Inventory Category Type.
        public virtual InventoryCategoryTypeViewModel CreateInventoryCategoryType(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel)
        {
            try
            {
                InventoryCategoryTypeResponse response = _inventoryCategoryTypeClient.CreateInventoryCategoryType(inventoryCategoryTypeViewModel.ToModel<InventoryCategoryTypeModel>());
                InventoryCategoryTypeModel inventoryCategoryTypeModel = response?.InventoryCategoryTypeModel;
                return IsNotNull(inventoryCategoryTypeModel) ? inventoryCategoryTypeModel.ToViewModel<InventoryCategoryTypeViewModel>() : new InventoryCategoryTypeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryCategoryTypeViewModel)GetViewModelWithErrorMessage(inventoryCategoryTypeViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryCategoryTypeViewModel)GetViewModelWithErrorMessage(inventoryCategoryTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return (InventoryCategoryTypeViewModel)GetViewModelWithErrorMessage(inventoryCategoryTypeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Inventory Category Type by  InventoryCategoryType master id.
        public virtual InventoryCategoryTypeViewModel GetInventoryCategoryType(byte inventoryCategoryTypeMasterId)
        {
            InventoryCategoryTypeResponse response = _inventoryCategoryTypeClient.GetInventoryCategoryType(inventoryCategoryTypeMasterId);
            return response?.InventoryCategoryTypeModel.ToViewModel<InventoryCategoryTypeViewModel>();
        }

        //Update InventoryCategoryType.
        public virtual InventoryCategoryTypeViewModel UpdateInventoryCategoryType(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Info);
                InventoryCategoryTypeResponse response = _inventoryCategoryTypeClient.UpdateInventoryCategoryType(inventoryCategoryTypeViewModel.ToModel<InventoryCategoryTypeModel>());
                InventoryCategoryTypeModel inventoryCategoryTypeModel = response?.InventoryCategoryTypeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryCategoryTypeModel) ? inventoryCategoryTypeModel.ToViewModel<InventoryCategoryTypeViewModel>() : (InventoryCategoryTypeViewModel)GetViewModelWithErrorMessage(new InventoryCategoryTypeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return (InventoryCategoryTypeViewModel)GetViewModelWithErrorMessage(inventoryCategoryTypeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryCategoryType.
        public virtual bool DeleteInventoryCategoryType(string inventoryCategoryTypeMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryCategoryTypeClient.DeleteInventoryCategoryType(new ParameterModel { Ids = inventoryCategoryTypeMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryCategoryType;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
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
                ColumnName = "Category Type Name",
                ColumnCode = "CategoryTypeName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
