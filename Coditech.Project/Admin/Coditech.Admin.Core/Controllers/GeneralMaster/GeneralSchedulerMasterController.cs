using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class GeneralTaskSchedulerMasterController : BaseController
    {
        private readonly IGeneralTaskSchedulerMasterAgent _generalTaskSchedulerMasterAgent;
        private const string createEditTaskScheduler = "~/Views/GeneralMaster/TaskScheduler/CreateEditTaskSchedulerMaster.cshtml";

        public GeneralTaskSchedulerMasterController(IGeneralTaskSchedulerMasterAgent generalTaskSchedulerMasterAgent)
        {
            _generalTaskSchedulerMasterAgent = generalTaskSchedulerMasterAgent;
        }

        #region TaskScheduler

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            TaskSchedulerListViewModel list = _generalTaskSchedulerMasterAgent.GetTaskSchedulerList(dataTableModel);
           
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/TaskScheduler/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/TaskScheduler/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreateTaskScheduler(string schedulerCallFor)
        {

            TaskSchedulerViewModel taskSchedulerViewModel = new TaskSchedulerViewModel();
            taskSchedulerViewModel.SchedulerCallFor = schedulerCallFor;
            taskSchedulerViewModel.SelectedWeekDays = !string.IsNullOrEmpty(taskSchedulerViewModel.WeekDays) ? taskSchedulerViewModel.WeekDays.Split(',').ToList() : new List<string>();

            taskSchedulerViewModel.SchedulerWeekDaysList = CoditechDropdownHelper.GeneralDropdownList(new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.SchedulerWeeks.ToString(),
                DropdownSelectedValue = taskSchedulerViewModel.WeekDays
            }).DropdownList;

            if (string.IsNullOrEmpty(taskSchedulerViewModel.SchedulerFrequency))
            {
                taskSchedulerViewModel.SchedulerFrequency = SchedulerFrequencyEnum.OneTime.ToString();
            }
            return View(createEditTaskScheduler, taskSchedulerViewModel);     
        }

        [HttpPost]
        public virtual ActionResult CreateTaskScheduler(TaskSchedulerViewModel taskSchedulerViewModel)
        {
            if (ModelState.IsValid)
            {
                taskSchedulerViewModel = _generalTaskSchedulerMasterAgent.CreateTaskScheduler(taskSchedulerViewModel);
                if (!taskSchedulerViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List");
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(taskSchedulerViewModel.ErrorMessage));
            return View(createEditTaskScheduler, taskSchedulerViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateTaskSchedulerDetails(int configuratorId,string schedulerCallFor)
        {
            TaskSchedulerViewModel taskSchedulerViewModel = _generalTaskSchedulerMasterAgent.GetTaskSchedulerDetails(configuratorId, schedulerCallFor);

            taskSchedulerViewModel.SelectedWeekDays = !string.IsNullOrEmpty(taskSchedulerViewModel.WeekDays) ? taskSchedulerViewModel.WeekDays.Split(',').ToList() : new List<string>();

            taskSchedulerViewModel.SchedulerWeekDaysList = CoditechDropdownHelper.GeneralDropdownList(new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.SchedulerWeeks.ToString(),
                DropdownSelectedValue = taskSchedulerViewModel.WeekDays
            }).DropdownList;

            if (string.IsNullOrEmpty(taskSchedulerViewModel.SchedulerFrequency))
            {
                taskSchedulerViewModel.SchedulerFrequency = SchedulerFrequencyEnum.OneTime.ToString();
            }
            return ActionView(createEditTaskScheduler, taskSchedulerViewModel);
        } 

        [HttpPost]
        public virtual ActionResult UpdateTaskSchedulerDetails(TaskSchedulerViewModel taskSchedulerViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalTaskSchedulerMasterAgent.UpdateTaskSchedulerDetails(taskSchedulerViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateTaskSchedulerDetails", new { configuratorId = taskSchedulerViewModel.TaskSchedulerMasterId, schedulerCallFor = taskSchedulerViewModel.SchedulerCallFor });
            }
            return View(createEditTaskScheduler, taskSchedulerViewModel);
        }

        public virtual ActionResult Delete(string taskSchedulerMasterIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taskSchedulerMasterIds))
            {
                status = _generalTaskSchedulerMasterAgent.DeleteTaskScheduler(taskSchedulerMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
        }

        [HttpGet]
        public virtual ActionResult ExecuteTaskScheduler(DateTime startTime)
        {
            TaskSchedulerViewModel taskSchedulerViewModel = _generalTaskSchedulerMasterAgent.GetExecuteTaskScheduler(startTime);
            return ActionView(createEditTaskScheduler, taskSchedulerViewModel);
        }

        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }
        #endregion

    }
}


