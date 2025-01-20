using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ILogMessageService
    {
        LogMessageListModel GetLogMessageList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        LogMessageModel GetLogMessage(long logMessageId);
        bool DeleteLogMessage(ParameterModel parameterModel);
    }
}
