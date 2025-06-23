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

namespace Coditech.Admin.Agents
{
    public class GeneralRunningNumbersAgent : BaseAgent, IGeneralRunningNumbersAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralRunningNumbersClient _generalRunningNumbersClient;
        #endregion

        #region Public Constructor
        public GeneralRunningNumbersAgent(ICoditechLogging coditechLogging, IGeneralRunningNumbersClient generalRunningNumbersClient)
        {
            _coditechLogging = coditechLogging;
            _generalRunningNumbersClient = GetClient<IGeneralRunningNumbersClient>(generalRunningNumbersClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralRunningNumbersListViewModel GetRunningNumbersList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("IsActive", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralRunningNumbersListResponse response = _generalRunningNumbersClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralRunningNumbersListModel organisationCentrewiseDepartmentList = new GeneralRunningNumbersListModel { GeneralRunningNumbersList = response?.GeneralRunningNumbersList };
            GeneralRunningNumbersListViewModel listViewModel = new GeneralRunningNumbersListViewModel();
            listViewModel.GeneralRunningNumbersList = organisationCentrewiseDepartmentList?.GeneralRunningNumbersList?.ToViewModel<GeneralRunningNumbersViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralRunningNumbersList.Count, BindColumns());
            return listViewModel;
        }

        //Create GeneralRunningNumbers.
        public virtual GeneralRunningNumbersViewModel CreateRunningNumbers(GeneralRunningNumbersViewModel generalRunningNumbersViewModel)
        {
            try
            {
                GeneralRunningNumbersResponse response = _generalRunningNumbersClient.CreateRunningNumbers(generalRunningNumbersViewModel.ToModel<GeneralRunningNumbersModel>());
                GeneralRunningNumbersModel generalRunningNumbersModel = response?.GeneralRunningNumbersModel;
                return HelperUtility.IsNotNull(generalRunningNumbersModel) ? generalRunningNumbersModel.ToViewModel<GeneralRunningNumbersViewModel>() : new GeneralRunningNumbersViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralRunningNumbersViewModel)GetViewModelWithErrorMessage(generalRunningNumbersViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralRunningNumbersViewModel)GetViewModelWithErrorMessage(generalRunningNumbersViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return (GeneralRunningNumbersViewModel)GetViewModelWithErrorMessage(generalRunningNumbersViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get General Running Numbers by general Running Number id.
        public virtual GeneralRunningNumbersViewModel GetRunningNumbers(long generalRunningNumberId)
        {
            GeneralRunningNumbersResponse response = _generalRunningNumbersClient.GetRunningNumbers(generalRunningNumberId);
            return response?.GeneralRunningNumbersModel.ToViewModel<GeneralRunningNumbersViewModel>();
        }

        //Update generalRunningNumbers.
        public virtual GeneralRunningNumbersViewModel UpdateRunningNumbers(GeneralRunningNumbersViewModel generalRunningNumbersViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Info);
                GeneralRunningNumbersResponse response = _generalRunningNumbersClient.UpdateRunningNumbers(generalRunningNumbersViewModel.ToModel<GeneralRunningNumbersModel>());
                GeneralRunningNumbersModel generalRunningNumbersModel = response?.GeneralRunningNumbersModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(generalRunningNumbersModel) ? generalRunningNumbersModel.ToViewModel<GeneralRunningNumbersViewModel>() : (GeneralRunningNumbersViewModel)GetViewModelWithErrorMessage(new GeneralRunningNumbersViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return (GeneralRunningNumbersViewModel)GetViewModelWithErrorMessage(generalRunningNumbersViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalRunningNumbers.
        public virtual bool DeleteRunningNumbers(string generalRunningNumberId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalRunningNumbersClient.DeleteRunningNumbers(new ParameterModel { Ids = generalRunningNumberId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralRunningNumbers;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
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
                ColumnName = "Description",
                ColumnCode = "Description",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Display Format",
                ColumnCode = "DisplayFormat",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Prefix",
                ColumnCode = "Prefix",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Separator",
                ColumnCode = "Separator",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Start Sequence",
                ColumnCode = "StartSequence",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Current Sequnce",
                ColumnCode = "CurrentSequnce",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Sequence Reset",
                ColumnCode = "IsSequenceReset",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
