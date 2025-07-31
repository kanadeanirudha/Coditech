using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralBatchMasterController : BaseController
    {
        private readonly IGeneralBatchAgent _generalBatchAgent;
        private const string createEditBatch = "~/Views/GeneralMaster/GeneralBatchMaster/CreateEditGeneralBatch.cshtml";
        private const string createEditTaskScheduler = "~/Views/GeneralMaster/GeneralBatchMaster/CreateEditTaskScheduler.cshtml";

        public GeneralBatchMasterController(IGeneralBatchAgent generalBatchAgent)
        {
            _generalBatchAgent = generalBatchAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralBatchListViewModel list = new GeneralBatchListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _generalBatchAgent.GetBatchList(dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralBatchMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralBatchMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            GeneralBatchViewModel generalBatchViewModel = new GeneralBatchViewModel();
            generalBatchViewModel.SelectedWeekDays = !string.IsNullOrEmpty(generalBatchViewModel.WeekDays) ? generalBatchViewModel.WeekDays.Split(',').ToList() : new List<string>();

            generalBatchViewModel.SchedulerWeekDaysList = CoditechDropdownHelper.GeneralDropdownList(new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.SchedulerWeeks.ToString(),
                DropdownSelectedValue = generalBatchViewModel.WeekDays
            }).DropdownList;

            if (string.IsNullOrEmpty(generalBatchViewModel.BatchFrequency))
            {
                generalBatchViewModel.BatchFrequency = SchedulerFrequencyEnum.Daily.ToString();
            }
            return View(createEditBatch, generalBatchViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralBatchViewModel generalBatchViewModel)
        {

            if (ModelState.IsValid)
            {
                BindDuration(generalBatchViewModel);
                generalBatchViewModel = _generalBatchAgent.CreateGeneralBatch(generalBatchViewModel);
                if (!generalBatchViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new { selectedCentreCode = generalBatchViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalBatchViewModel.ErrorMessage));
            return View(createEditBatch, generalBatchViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateGeneralBatch(int generalBatchMasterId)
        {
            GeneralBatchViewModel generalBatchViewModel = _generalBatchAgent.GetGeneralBatch(generalBatchMasterId);

            generalBatchViewModel.SelectedWeekDays = !string.IsNullOrEmpty(generalBatchViewModel.WeekDays)? generalBatchViewModel.WeekDays.Split(',').ToList() : new List<string>();

            generalBatchViewModel.SchedulerWeekDaysList = CoditechDropdownHelper.GeneralDropdownList(new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.SchedulerWeeks.ToString(),
                DropdownSelectedValue = generalBatchViewModel.WeekDays
            }).DropdownList;
            return ActionView(createEditBatch, generalBatchViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateGeneralBatch(GeneralBatchViewModel generalBatchViewModel)
        {
            if (ModelState.IsValid)
            {
                BindDuration(generalBatchViewModel);
                SetNotificationMessage(_generalBatchAgent.UpdateGeneralBatch(generalBatchViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateGeneralBatch", new { generalBatchMasterId = generalBatchViewModel.GeneralBatchMasterId });
            }
            return View(createEditBatch, generalBatchViewModel);
        }

        public virtual ActionResult Delete(string generalBatchMasterIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalBatchMasterIds))
            {
                status = _generalBatchAgent.DeleteGeneralBatch(generalBatchMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(string.IsNullOrEmpty(message) ? GeneralResources.DeleteErrorMessage: message)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
        }

        public virtual ActionResult GetGeneralBatchUserList(DataTableViewModel dataTableViewModel)
        {
            GeneralBatchUserListViewModel list = _generalBatchAgent.GetGeneralBatchUserList(Convert.ToInt32(dataTableViewModel.SelectedParameter1), Convert.ToString(dataTableViewModel.SelectedParameter2), dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralBatchMaster/GeneralBatchUser/_AssociatedBatchList.cshtml", list);
            }
            list.SelectedParameter1 = dataTableViewModel.SelectedParameter1;
            list.SelectedParameter2 = dataTableViewModel.SelectedParameter2;

            return View($"~/Views/GeneralMaster/GeneralBatchMaster/GeneralBatchUser/AssociatedBatchList.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetAssociateUnAssociateBatchwiseUser(GeneralBatchUserViewModel generalBatchUserViewModel)
        {
            return PartialView("~/Views/GeneralMaster/GeneralBatchMaster/GeneralBatchUser/_AssociateUnAssociateBatchwiseUser.cshtml", generalBatchUserViewModel);
        }

        [HttpPost]
        public virtual ActionResult AssociateUnAssociateBatchwiseUser(GeneralBatchUserViewModel generalBatchUserViewModel)
        {
            SetNotificationMessage(_generalBatchAgent.AssociateUnAssociateBatchwiseUser(generalBatchUserViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            return RedirectToAction("GetGeneralBatchUserList", new DataTableViewModel { SelectedParameter1 = generalBatchUserViewModel.GeneralBatchMasterId.ToString(), SelectedParameter2 = generalBatchUserViewModel.UserType });
        }

        #region 
        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }

        protected void BindDuration(GeneralBatchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.DurationHours) && !string.IsNullOrEmpty(model.DurationMinutes))
            {
                string durationString = $"{model.DurationHours}:{model.DurationMinutes}:00";
                if (TimeSpan.TryParse(durationString, out var duration))
                {
                    model.Duration = duration;
                }
            }
        }
        #endregion

    }
}


