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
    public class GeneralFinancialYearAgent : BaseAgent, IGeneralFinancialYearAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralFinancialYearClient _generalFinancialYearClient;
        #endregion

        #region Public Constructor
        public GeneralFinancialYearAgent(ICoditechLogging coditechLogging, IGeneralFinancialYearClient generalFinancialYearClient)
        {
            _coditechLogging = coditechLogging;
            _generalFinancialYearClient = GetClient<IGeneralFinancialYearClient>(generalFinancialYearClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralFinancialYearListViewModel GetFinancialYearList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            {
                filters.Add("FromDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Todate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FromDate" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralFinancialYearListResponse response = _generalFinancialYearClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralFinancialYearListModel FinancialYearList = new GeneralFinancialYearListModel { GeneralFinancialYearList = response?.GeneralFinancialYearList };
            GeneralFinancialYearListViewModel listViewModel = new GeneralFinancialYearListViewModel();
            listViewModel.GeneralFinancialYearList = FinancialYearList?.GeneralFinancialYearList?.ToViewModel<GeneralFinancialYearViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralFinancialYearList.Count, BindColumns());
            return listViewModel;
        }

        //Create General FinancialYear.
        public virtual GeneralFinancialYearViewModel CreateFinancialYear(GeneralFinancialYearViewModel generalFinancialYearViewModel)
        {
            try
            {
                GeneralFinancialYearResponse response = _generalFinancialYearClient.CreateFinancialYear(generalFinancialYearViewModel.ToModel<GeneralFinancialYearModel>());
                GeneralFinancialYearModel generalFinancialYearModel = response?.GeneralFinancialYearModel;
                return IsNotNull(generalFinancialYearModel) ? generalFinancialYearModel.ToViewModel<GeneralFinancialYearViewModel>() : new GeneralFinancialYearViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general FinancialYear by general FinancialYear master id.
        public virtual GeneralFinancialYearViewModel GetFinancialYear(short generalFinancialYearId)
        {
            GeneralFinancialYearResponse response = _generalFinancialYearClient.GetFinancialYear(generalFinancialYearId);
            return response?.GeneralFinancialYearModel.ToViewModel<GeneralFinancialYearViewModel>();
        }

        //Update generalFinancialYear.
        public virtual GeneralFinancialYearViewModel UpdateFinancialYear(GeneralFinancialYearViewModel generalFinancialYearViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Info);
                GeneralFinancialYearResponse response = _generalFinancialYearClient.UpdateFinancialYear(generalFinancialYearViewModel.ToModel<GeneralFinancialYearModel>());
                GeneralFinancialYearModel generalFinancialYearModel = response?.GeneralFinancialYearModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalFinancialYearModel) ? generalFinancialYearModel.ToViewModel<GeneralFinancialYearViewModel>() : (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(new GeneralFinancialYearViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return (GeneralFinancialYearViewModel)GetViewModelWithErrorMessage(generalFinancialYearViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalFinancialYear.
        public virtual bool DeleteFinancialYear(string generalFinancialYearId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalFinancialYearClient.DeleteFinancialYear(new ParameterModel { Ids = generalFinancialYearId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralFinancialYearMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Financial Year From Date",
                ColumnCode = "FromDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Financial Year To Date",
                ColumnCode = "ToDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Year End",
                ColumnCode = "IsYearEnd",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Current Financial Year",
                ColumnCode = "IsCurrentFinancialYear",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all FinancialYear list from database 
        public virtual GeneralFinancialYearListResponse GetFinancialYearList()
        {
            GeneralFinancialYearListResponse FinancialYearList = _generalFinancialYearClient.List(null, null, null, 1, int.MaxValue);
            return FinancialYearList?.GeneralFinancialYearList?.Count > 0 ? FinancialYearList : new GeneralFinancialYearListResponse();
        }
        #endregion
    }
}
