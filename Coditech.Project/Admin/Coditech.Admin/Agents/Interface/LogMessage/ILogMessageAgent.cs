using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface ILogMessageAgent
    {
        /// <summary>
        /// Get list of Log Message.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>LogMessageListViewModel</returns>
        LogMessageListViewModel GetLogMessageList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get LogMessage by logMessageId.
        /// </summary>
        /// <param name="logMessageId">logMessageId</param>
        /// <returns>Returns LogMessageViewModel.</returns>
        LogMessageViewModel GetLogMessage(long logMessageId);

        /// <summary>
        /// Delete LogMessage.
        /// </summary>
        /// <param name="logMessageId">logMessageId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteLogMessage(string logMessageId, out string errorMessage);
    }
}
