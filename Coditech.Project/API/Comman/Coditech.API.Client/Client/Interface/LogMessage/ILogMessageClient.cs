using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ILogMessageClient : IBaseClient
    {
        /// <summary>
        /// Get list of Log Message.
        /// </summary>
        /// <returns>LogMessageListResponse</returns>
        LogMessageListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get LogMessage by logMessageId.
        /// </summary>
        /// <param name="logMessageId">logMessageId</param>
        /// <returns>Returns LogMessageResponse.</returns>
        LogMessageResponse GetLogMessage(long logMessageId);

        /// <summary>
        /// Delete Log Message.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteLogMessage(ParameterModel body);
    }
}
