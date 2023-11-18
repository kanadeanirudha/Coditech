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
    public class GeneralTaxMasterAgent : BaseAgent, IGeneralTaxMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralTaxMasterClient _generalTaxMasterClient;
        #endregion

        #region Public Constructor
        public GeneralTaxMasterAgent(ICoditechLogging coditechLogging, IGeneralTaxMasterClient generalTaxMasterClient)
        {
            _coditechLogging = coditechLogging;
            _generalTaxMasterClient = GetClient<IGeneralTaxMasterClient>(generalTaxMasterClient);
        }
        #endregion

        #region Public Methods
        public GeneralTaxMasterListViewModel GetTaxMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaxName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaxRate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "TaxName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralTaxMasterListResponse response = _generalTaxMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralTaxMasterListModel taxMasterList = new GeneralTaxMasterListModel { GeneralTaxMasterList = response?.GeneralTaxMasterList };
            GeneralTaxMasterListViewModel listViewModel = new GeneralTaxMasterListViewModel();
            listViewModel.GeneralTaxMasterList = taxMasterList?.GeneralTaxMasterList?.ToViewModel<GeneralTaxMasterViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralTaxMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create General TaxMaster.
        public virtual GeneralTaxMasterViewModel CreateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            try
            {
                GeneralTaxMasterResponse response = _generalTaxMasterClient.CreateTaxMaster(generalTaxMasterViewModel.ToModel<GeneralTaxMasterModel>());
                GeneralTaxMasterModel generalTaxMasterModel = response?.GeneralTaxModel;
                return IsNotNull(generalTaxMasterModel) ? generalTaxMasterModel.ToViewModel<GeneralTaxMasterViewModel>() : new GeneralTaxMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get generalTaxMaster by general tax master id.
        public virtual GeneralTaxMasterViewModel GetTaxMaster(short taxMasterId)
        {
            GeneralTaxMasterResponse response = _generalTaxMasterClient.GetTaxMaster(taxMasterId);
            return response?.GeneralTaxModel.ToViewModel<GeneralTaxMasterViewModel>();
        }

        //Update TaxMaster.
        public virtual GeneralTaxMasterViewModel UpdateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Info);
                GeneralTaxMasterResponse response = _generalTaxMasterClient.UpdateTaxMaster(generalTaxMasterViewModel.ToModel<GeneralTaxMasterModel>());
                GeneralTaxMasterModel generalTaxMasterModel = response?.GeneralTaxModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalTaxMasterModel) ? generalTaxMasterModel.ToViewModel<GeneralTaxMasterViewModel>() : (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(new GeneralTaxMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete TaxMaster.
        public virtual bool DeleteTaxMaster(string taxMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalTaxMasterClient.DeleteTaxMaster(new ParameterModel { Ids = taxMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralTaxMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Tax Name",
                ColumnCode = "TaxName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Tax Rate",
                ColumnCode = "TaxRate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Other State",
                ColumnCode = "IsOtherState",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all tax list from database 
        public GeneralTaxMasterListResponse GetAllTaxList()
        {
            GeneralTaxMasterListResponse taxMasterList = _generalTaxMasterClient.List(null, null,null,1,int.MaxValue);
            return taxMasterList?.GeneralTaxMasterList?.Count > 0 ? taxMasterList : new GeneralTaxMasterListResponse();
        }
        #endregion
    }
}
