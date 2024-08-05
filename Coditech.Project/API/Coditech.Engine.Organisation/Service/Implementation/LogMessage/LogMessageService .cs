using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class LogMessageService : ILogMessageService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<LogMessage> _logMessageRepository;
        public LogMessageService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _logMessageRepository = new CoditechRepository<LogMessage>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual LogMessageListModel GetLogMessageList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<LogMessageModel> objStoredProc = new CoditechViewRepository<LogMessageModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<LogMessageModel> LogMessageList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetLogMessageList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            LogMessageListModel listModel = new LogMessageListModel();

            listModel.LogMessageList = LogMessageList?.Count > 0 ? LogMessageList : new List<LogMessageModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get LogMessage by log Message Id.
        public virtual LogMessageModel GetLogMessage(long logMessageId)
        {
            if (logMessageId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LogMessageID"));

            //Get the LogMessage Details based on id.
            LogMessage logMessage = _logMessageRepository.Table.FirstOrDefault(x => x.LogMessageId == logMessageId);
            LogMessageModel logMessageModel = logMessage?.FromEntityToModel<LogMessageModel>();
            return logMessageModel;
        }

        //Delete LogMessage.
        public virtual bool DeleteLogMessage(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LogMessageID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("LogMessageId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteLogMessage @LogMessageId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}
