using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Coditech.Admin.Controllers
{
    public class TaskApprovalSettingController : BaseController
    {
        private readonly ITaskApprovalSettingAgent _taskApprovalSettingAgent;
        private const string createEditTaskApprovalSetting = "~/Views/GeneralMaster/TaskApprovalSetting/CreateEditTaskApprovalSetting.cshtml";
        private const string EditTaskApprovalSetting = "~/Views/GeneralMaster/TaskApprovalSetting/EditTaskApprovalSetting.cshtml";

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

        [HttpGet]
        public virtual ActionResult AddUpdateTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            TaskApprovalSettingViewModel taskApprovalSettingViewModel = _taskApprovalSettingAgent.GetTaskApprovalSetting(taskMasterId, centreCode);
            return ActionView(createEditTaskApprovalSetting, taskApprovalSettingViewModel);
        }       

        [HttpGet]
        public virtual ActionResult GetEmployeeListByCentreCode(string centreCode, byte countNumber)
        {
            TaskApprovalSettingViewModel taskApprovalSettingViewModel = new TaskApprovalSettingViewModel() { CountNumber = countNumber };
            taskApprovalSettingViewModel.EmployeeList = _taskApprovalSettingAgent.GetEmployeeListByCentreCode(centreCode);

            List<SelectListItem> employeeList = new List<SelectListItem>();

            employeeList.Add(new SelectListItem { Text = "--------Select--------", Value = "" });

            foreach (EmployeeMasterModel item in taskApprovalSettingViewModel?.EmployeeList)
            {
                employeeList.Add(new SelectListItem { Text = $"{item.FirstName} {item.LastName}({item.PersonCode}-{item.DepartmentName})", Value = item.EmployeeId.ToString() });
            }

            ViewData["EmployeeList"] = employeeList;

            return PartialView($"~/Views/GeneralMaster/TaskApprovalSetting/_CreateTaskApprovalSettingEmployeeList.cshtml", taskApprovalSettingViewModel);
        }
       
        [HttpPost]
        public virtual ActionResult AddUpdateTaskApprovalSetting(TaskApprovalSettingViewModel taskApprovalSettingViewModel)
        {
            
            if (ModelState.IsValid)
            {

                taskApprovalSettingViewModel = _taskApprovalSettingAgent.AddUpdateTaskApprovalSetting(taskApprovalSettingViewModel);
                if (!taskApprovalSettingViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                }
                else
                {
                    SetNotificationMessage(GetErrorNotificationMessage(taskApprovalSettingViewModel.ErrorMessage));
                }
            }
            else
            {
                SetNotificationMessage(GetErrorNotificationMessage(taskApprovalSettingViewModel.ErrorMessage));
            }
            return RedirectToAction("AddUpdateTaskApprovalSetting", new { taskMasterId = taskApprovalSettingViewModel.TaskMasterId, centreCode = taskApprovalSettingViewModel.CentreCode });
        }

        [HttpGet]
        public virtual ActionResult UpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId)
        {
            TaskApprovalSettingViewModel taskApprovalSettingViewModel = _taskApprovalSettingAgent.GetUpdateTaskApprovalSetting(taskMasterId, centreCode, taskApprovalSettingId);
            return ActionView(EditTaskApprovalSetting, taskApprovalSettingViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateTaskApprovalSetting(TaskApprovalSettingViewModel taskApprovalSettingViewModel)
        {

            if (ModelState.IsValid)
            {
                SetNotificationMessage(_taskApprovalSettingAgent.UpdateTaskApprovalSetting(taskApprovalSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateTaskApprovalSetting", new { taskMasterId = taskApprovalSettingViewModel.TaskMasterId, centreCode = taskApprovalSettingViewModel.CentreCode });
            }
            return View(EditTaskApprovalSetting, taskApprovalSettingViewModel);
        }

        public virtual ActionResult Delete(string taskApprovalSettingIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taskApprovalSettingIds))
            {
                status = _taskApprovalSettingAgent.DeleteTaskApprovalSetting(taskApprovalSettingIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<TaskApprovalSettingController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<TaskApprovalSettingController>(x => x.List(null));
        }


        public virtual ActionResult Cancel(string selectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = selectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }
    }
}













