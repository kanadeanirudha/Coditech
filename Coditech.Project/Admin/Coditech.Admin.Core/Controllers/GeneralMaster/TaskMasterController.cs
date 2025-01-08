using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class TaskMasterController : BaseController
    {
        private readonly ITaskMasterAgent _taskMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/TaskMaster/CreateEdit.cshtml";
        public TaskMasterController(ITaskMasterAgent taskMasterAgent)
        {
            _taskMasterAgent = taskMasterAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            TaskMasterListViewModel list = _taskMasterAgent.GetTaskMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/TaskMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/TaskMaster/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new TaskMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(TaskMasterViewModel taskMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                taskMasterViewModel = _taskMasterAgent.CreateTaskMaster(taskMasterViewModel);
                if (!taskMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(taskMasterViewModel.ErrorMessage));
            return View(createEdit, taskMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short taskMasterId)
        {
            TaskMasterViewModel taskMasterViewModel = _taskMasterAgent.GetTaskMaster(taskMasterId);
            return ActionView(createEdit, taskMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(TaskMasterViewModel taskMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                taskMasterViewModel = _taskMasterAgent.UpdateTaskMaster(taskMasterViewModel);
                SetNotificationMessage(taskMasterViewModel.HasError
                ? GetErrorNotificationMessage(taskMasterViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { taskMasterId = taskMasterViewModel.TaskMasterId });
            }
            return View(createEdit, taskMasterViewModel);
        }
        public virtual ActionResult Delete(string taskMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taskMasterIds))
            {
                status = _taskMasterAgent.DeleteTaskMaster(taskMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<TaskMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<TaskMasterController>(x => x.List(null));
        }
        #region Protected

        #endregion
    }
}
