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
    public class LogMessageAgent : BaseAgent, ILogMessageAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ILogMessageClient _logMessageClient;
        #endregion

        #region Public Constructor
        public LogMessageAgent(ICoditechLogging coditechLogging, ILogMessageClient logMessageClient)
        {
            _coditechLogging = coditechLogging;
            _logMessageClient = GetClient<ILogMessageClient>(logMessageClient);
        }
        #endregion

        #region Public Methods
        public virtual LogMessageListViewModel GetLogMessageList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FileName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MethodName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FileName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            LogMessageListResponse response = _logMessageClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            LogMessageListModel countryList = new LogMessageListModel { LogMessageList = response?.LogMessageList };
            LogMessageListViewModel listViewModel = new LogMessageListViewModel();
            listViewModel.LogMessageList = countryList?.LogMessageList?.ToViewModel<LogMessageViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.LogMessageList.Count, BindColumns());
            return listViewModel;
        }

        //Get Log Message by general log message id.
        public virtual LogMessageViewModel GetLogMessage(long logMessageId)
        {
            LogMessageResponse response = _logMessageClient.GetLogMessage(logMessageId);
            return response?.LogMessageModel.ToViewModel<LogMessageViewModel>();
        }

        //Delete log Message.
        public virtual bool DeleteLogMessage(string logMessageId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _logMessageClient.DeleteLogMessage(new ParameterModel { Ids = logMessageId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteLogMessage;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Error);
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
                ColumnName = "Error Message",
                ColumnCode = "ErrorMessageType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Component Name",
                ColumnCode = "ComponentName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Exception Message",
                ColumnCode = "ExceptionMessage",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
