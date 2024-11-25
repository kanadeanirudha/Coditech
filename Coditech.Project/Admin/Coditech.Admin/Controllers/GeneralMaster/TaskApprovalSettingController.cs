using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class TaskApprovalSettingController : BaseController
    {
        private readonly ITaskApprovalSettingAgent _taskApprovalSettingAgent;
        private const string createEditTaskApprovalSetting = "~/Views/GeneralMaster/TaskApprovalSetting/CreateEditTaskApprovalSetting.cshtml";

        public TaskApprovalSettingController(ITaskApprovalSettingAgent taskApprovalSettingAgent)
        {
            _taskApprovalSettingAgent = taskApprovalSettingAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            TaskApprovalSettingListViewModel list = new TaskApprovalSettingListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _taskApprovalSettingAgent.GetTaskApprovalSettingList(dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/TaskApprovalSetting/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/TaskApprovalSetting/List.cshtml", list);
        }
     
        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode};
            return RedirectToAction("List", dataTableViewModel);
        }
    }
}