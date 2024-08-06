using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class LogMessageController : BaseController
    {
        private readonly ILogMessageAgent _logMessageAgent;
        private const string logMessage = "~/Views/LogMessage/LogMessage.cshtml";

        public LogMessageController(ILogMessageAgent logMessageAgent)
        {
            _logMessageAgent = logMessageAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            LogMessageListViewModel list = _logMessageAgent.GetLogMessageList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/LogMessage/_List.cshtml", list);
            }
            return View($"~/Views/LogMessage/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetLogMessage(long logMessageId)
        {
            LogMessageViewModel logMessageViewModel = _logMessageAgent.GetLogMessage(logMessageId);
            return ActionView(logMessage, logMessageViewModel);
        }

        public virtual ActionResult Delete(string logMessageIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(logMessageIds))
            {
                status = _logMessageAgent.DeleteLogMessage(logMessageIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<LogMessageController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<LogMessageController>(x => x.List(null));
        }

        #region Protected
        #endregion
    }
}
