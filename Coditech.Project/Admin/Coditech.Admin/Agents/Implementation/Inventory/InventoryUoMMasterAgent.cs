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
    public class InventoryUoMMasterAgent : BaseAgent, IInventoryUoMMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IInventoryUoMMasterClient _inventoryUoMMasterClient;
        #endregion

        #region Public Constructor
        public InventoryUoMMasterAgent(ICoditechLogging coditechLogging, IInventoryUoMMasterClient inventoryUoMMasterClient)
        {
            _coditechLogging = coditechLogging;
            _inventoryUoMMasterClient = GetClient<IInventoryUoMMasterClient>(inventoryUoMMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual InventoryUoMMasterListViewModel GetInventoryUoMMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("UomCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UoMDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CommercialDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MeasurementUnitDisplayName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ConvertionFactor", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("AdditiveConstant", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DecimalPlacesUpto", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DecimalRounding", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "UomCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            InventoryUoMMasterListResponse response = _inventoryUoMMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            InventoryUoMMasterListModel InventoryUoMMasterList = new InventoryUoMMasterListModel { InventoryUoMMasterList = response?.InventoryUoMMasterList };
            InventoryUoMMasterListViewModel listViewModel = new InventoryUoMMasterListViewModel();
            listViewModel.InventoryUoMMasterList = InventoryUoMMasterList?.InventoryUoMMasterList?.ToViewModel<InventoryUoMMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.InventoryUoMMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create General InventoryUoMMaster.
        public virtual InventoryUoMMasterViewModel CreateInventoryUoMMaster(InventoryUoMMasterViewModel inventoryUoMMasterViewModel)
        {
            try
            {
                InventoryUoMMasterResponse response = _inventoryUoMMasterClient.CreateInventoryUoMMaster(inventoryUoMMasterViewModel.ToModel<InventoryUoMMasterModel>());
                InventoryUoMMasterModel inventoryUoMMasterModel = response?.InventoryUoMMasterModel;
                return IsNotNull(inventoryUoMMasterModel) ? inventoryUoMMasterModel.ToViewModel<InventoryUoMMasterViewModel>() : new InventoryUoMMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (InventoryUoMMasterViewModel)GetViewModelWithErrorMessage(inventoryUoMMasterViewModel, ex.ErrorMessage);
                    default:
                        return (InventoryUoMMasterViewModel)GetViewModelWithErrorMessage(inventoryUoMMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return (InventoryUoMMasterViewModel)GetViewModelWithErrorMessage(inventoryUoMMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general InventoryUoMMaster by general InventoryUoMMaster master id.
        public virtual InventoryUoMMasterViewModel GetInventoryUoMMaster(short inventoryUoMMasterId)
        {
            InventoryUoMMasterResponse response = _inventoryUoMMasterClient.GetInventoryUoMMaster(inventoryUoMMasterId);
            return response?.InventoryUoMMasterModel.ToViewModel<InventoryUoMMasterViewModel>();
        }

        //Update InventoryUoMMaster.
        public virtual InventoryUoMMasterViewModel UpdateInventoryUoMMaster(InventoryUoMMasterViewModel InventoryUoMMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Info);
                InventoryUoMMasterResponse response = _inventoryUoMMasterClient.UpdateInventoryUoMMaster(InventoryUoMMasterViewModel.ToModel<InventoryUoMMasterModel>());
                InventoryUoMMasterModel inventoryUoMMasterModel = response?.InventoryUoMMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Info);
                return IsNotNull(inventoryUoMMasterModel) ? inventoryUoMMasterModel.ToViewModel<InventoryUoMMasterViewModel>() : (InventoryUoMMasterViewModel)GetViewModelWithErrorMessage(new InventoryUoMMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return (InventoryUoMMasterViewModel)GetViewModelWithErrorMessage(InventoryUoMMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete InventoryUoMMaster.
        public virtual bool DeleteInventoryUoMMaster(string InventoryUoMMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _inventoryUoMMasterClient.DeleteInventoryUoMMaster(new ParameterModel { Ids = InventoryUoMMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteInventoryUoMMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Uom Code",
                ColumnCode = "UomCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "UoM Description",
                ColumnCode = "UoMDescription",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Commercial Description",
                ColumnCode = "CommercialDescription",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Measurement Unit",
                ColumnCode = "MeasurementUnitDisplayName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Convertion Factor",
                ColumnCode = "ConvertionFactor",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Additive Constant",
                ColumnCode = "AdditiveConstant",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Decimal Places Upto",
                ColumnCode = "DecimalPlacesUpto",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Decimal Rounding",
                ColumnCode = "DecimalRounding",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all InventoryUoMMaster list from database 
        public virtual InventoryUoMMasterListResponse GetInventoryUoMMasterList()
        {
            InventoryUoMMasterListResponse InventoryUoMMasterList = _inventoryUoMMasterClient.List(null, null, null, 1, int.MaxValue);
            return InventoryUoMMasterList?.InventoryUoMMasterList?.Count > 0 ? InventoryUoMMasterList : new InventoryUoMMasterListResponse();
        }
        #endregion
    }
}
