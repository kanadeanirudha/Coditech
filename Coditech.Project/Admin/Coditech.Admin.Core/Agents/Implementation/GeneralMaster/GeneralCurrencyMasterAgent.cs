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
    public class GeneralCurrencyMasterAgent : BaseAgent, IGeneralCurrencyMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralCurrencyMasterClient _generalCurrencyMasterClient;
        #endregion

        #region Public Constructor
        public GeneralCurrencyMasterAgent(ICoditechLogging coditechLogging, IGeneralCurrencyMasterClient generalCurrencyMasterClient)
        {
            _coditechLogging = coditechLogging;
            _generalCurrencyMasterClient = GetClient<IGeneralCurrencyMasterClient>(generalCurrencyMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralCurrencyMasterListViewModel GetCurrencyList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CurrencyName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CurrencyCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CurrencySymbol", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CurrencyName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralCurrencyMasterListResponse response = _generalCurrencyMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCurrencyMasterListModel currencyList = new GeneralCurrencyMasterListModel { GeneralCurrencyMasterList = response?.GeneralCurrencyMasterList };
            GeneralCurrencyMasterListViewModel listViewModel = new GeneralCurrencyMasterListViewModel();
            listViewModel.GeneralCurrencyMasterList = currencyList?.GeneralCurrencyMasterList?.ToViewModel<GeneralCurrencyMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralCurrencyMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Country.
        public virtual GeneralCurrencyMasterViewModel CreateCurrency(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel)
        {
            try
            {
                GeneralCurrencyMasterResponse response = _generalCurrencyMasterClient.CreateCurrency(generalCurrencyMasterViewModel.ToModel<GeneralCurrencyMasterModel>());
                GeneralCurrencyMasterModel generalCurrencyMasterModel = response?.GeneralCurrencyMasterModel;
                return IsNotNull(generalCurrencyMasterModel) ? generalCurrencyMasterModel.ToViewModel<GeneralCurrencyMasterViewModel>() : new GeneralCurrencyMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Currency by general Currency master id.
        public virtual GeneralCurrencyMasterViewModel GetCurrency(short generalCurrencyMasterId)
        {
            GeneralCurrencyMasterResponse response = _generalCurrencyMasterClient.GetCurrency(generalCurrencyMasterId);
            return response?.GeneralCurrencyMasterModel.ToViewModel<GeneralCurrencyMasterViewModel>();
        }

        //Update generalCurrency.
        public virtual GeneralCurrencyMasterViewModel UpdateCurrency(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Info);
                GeneralCurrencyMasterResponse response = _generalCurrencyMasterClient.UpdateCurrency(generalCurrencyMasterViewModel.ToModel<GeneralCurrencyMasterModel>());
                GeneralCurrencyMasterModel generalCurrencyMasterModel = response?.GeneralCurrencyMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalCurrencyMasterModel) ? generalCurrencyMasterModel.ToViewModel<GeneralCurrencyMasterViewModel>() : (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(new GeneralCurrencyMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return (GeneralCurrencyMasterViewModel)GetViewModelWithErrorMessage(generalCurrencyMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalCurrency.
        public virtual bool DeleteCurrency(string generalCurrencyMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalCurrencyMasterClient.DeleteCurrency(new ParameterModel { Ids = generalCurrencyMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralCurrencyMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Currency Name",
                ColumnCode = "CurrencyName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Currency Code",
                ColumnCode = "CurrencyCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Currency Symbol",
                ColumnCode = "CurrencySymbol",
                IsSortable = true,
            });

            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all Currency list from database 
        public virtual GeneralCurrencyMasterListResponse GetCurrencyList()
        {
            GeneralCurrencyMasterListResponse CurrencyList = _generalCurrencyMasterClient.List(null, null, null, 1, int.MaxValue);
            return CurrencyList?.GeneralCurrencyMasterList?.Count > 0 ? CurrencyList : new GeneralCurrencyMasterListResponse();
        }
        #endregion
    }
}
